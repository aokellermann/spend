using System.Data;
using Going.Plaid;
using Going.Plaid.Transactions;
using Graph.Domain.Entities.Transactions;
using Graph.Infrastructure;
using HotChocolate.Authorization;
using HotChocolate.Language;
using MongoDB.Driver;

namespace Graph.GraphQL.Mutations.Transactions;

[ExtendObjectType(OperationType.Mutation)]
public class SyncTransactionsMutation
{
    [GraphQLName(nameof(SyncTransactions) + nameof(Request))]
    public class Request
    {
        public string ItemId { get; set; }
    }

    [GraphQLName(nameof(SyncTransactions) + nameof(Response))]
    public class Response
    {
        public TransactionsSyncResponse TransactionsSyncResponse { get; set; }
    }

    [Authorize]
    public async Task<Response> SyncTransactions(Request request, UserContext ctx, PlaidClient plaid, SpendDb db)
    {
        var item = await db.ItemLinks
            .Find(x => x.UserId == ctx.UserId!.Value && x.ItemId == request.ItemId)
            .FirstAsync();
        var state = await db.TransactionSyncStates
            .Find(x => x.UserId == ctx.UserId!.Value && x.ItemLinkId == item.Id)
            .FirstOrDefaultAsync();

        using var sesh = await db.Db.Client.StartSessionAsync();
        sesh.StartTransaction();

        var transactions = await plaid.TransactionsSyncAsync(new TransactionsSyncRequest
        {
            AccessToken = item.AccessToken,
            Cursor = state?.Cursor
        });

        var added = transactions.Added.Select(x => new InsertOneModel<Transaction>(new Transaction
        {
            ItemId = item.ItemId,
            PlaidTransactionId = x.TransactionId!,
            PlaidTransaction = x,
            UserId = ctx.UserId!.Value,
            InsertedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Version = 1
        }));

        var updated = transactions.Modified.Select(x =>
            new ReplaceOneModel<Transaction>(
                Builders<Transaction>.Filter.Eq(y => y.PlaidTransactionId, x.TransactionId), new Transaction
                {
                    ItemId = item.ItemId,
                    PlaidTransactionId = x.TransactionId!,
                    PlaidTransaction = x,
                    UserId = ctx.UserId!.Value,
                    InsertedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Version = 1
                })
            {
                IsUpsert = true
            });

        var removed = transactions.Removed.Select(x =>
            new DeleteOneModel<Transaction>(Builders<Transaction>.Filter.Eq(y => y.PlaidTransactionId,
                x.TransactionId)));

        var bulkOps = added.Cast<WriteModel<Transaction>>().Concat(updated).Concat(removed).ToArray();

        BulkWriteResult<Transaction>? bulkWriteResult = null;
        if (bulkOps.Length != 0)
        {
            bulkWriteResult = await db.Transactions.BulkWriteAsync(bulkOps);
        }

        var nextCursor = transactions.NextCursor;

        if (state is null)
        {
            await db.TransactionSyncStates.InsertOneAsync(new TransactionsSyncState
            {
                UserId = ctx.UserId!.Value,
                ItemLinkId = item.Id,
                Cursor = nextCursor,
                Version = 1,
                InsertedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
        else
        {
            var filter = Builders<TransactionsSyncState>.Filter.Empty;
            filter &= Builders<TransactionsSyncState>.Filter.Eq(x => x.UserId, ctx.UserId!.Value);
            filter &= Builders<TransactionsSyncState>.Filter.Eq(x => x.ItemLinkId, item.Id);
            filter &= Builders<TransactionsSyncState>.Filter.Eq(x => x.Version, state.Version);
            var updatedCount = await db.TransactionSyncStates
                .FindOneAndUpdateAsync(filter,
                    Builders<TransactionsSyncState>.Update
                        .Set(x => x.Cursor, nextCursor)
                        .Inc(x => x.Version, 1)
                        .CurrentDate(x => x.UpdatedAt));

            if (updatedCount is null)
            {
                throw new DBConcurrencyException("Failed to update sync transaction state");
            }
        }

        await sesh.CommitTransactionAsync();

        return new Response
        {
            TransactionsSyncResponse = transactions,
        };
    }
}