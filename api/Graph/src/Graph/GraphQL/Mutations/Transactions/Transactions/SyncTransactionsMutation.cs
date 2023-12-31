using System.Data;
using Going.Plaid;
using Going.Plaid.Transactions;
using HotChocolate.Language;
using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.Transactions.Transactions;

[ExtendObjectType(OperationType.Mutation)]
public class SyncTransactionsMutation
{
    /// <summary>
    ///     Sync transactions request.
    /// </summary>
    [GraphQLName(nameof(SyncTransactions) + nameof(Request))]
    public class Request
    {
        /// <summary>
        ///     The item link to sync.
        /// </summary>
        public ObjectId ItemLinkId { get; init; }
    }

    /// <summary>
    ///     Sync transactions response.
    /// </summary>
    [GraphQLName(nameof(SyncTransactions) + nameof(Response))]
    public class Response
    {
        /// <summary>
        ///     The plaid sync response.
        /// </summary>
        public TransactionsSyncResponse TransactionsSyncResponse { get; init; } = default!;
    }

    /// <summary>
    ///     Sync transactions for an item.
    /// </summary>
    public async Task<Response> SyncTransactions(Request request, UserContext ctx, PlaidClient plaid, SpendDb db)
    {
        var itemLink = await db.ItemLinks
            .Find(x => x.UserId == ctx.UserId!.Value && x.Id == request.ItemLinkId)
            .FirstAsync();
        var state = await db.TransactionSyncStates
            .Find(x => x.UserId == ctx.UserId!.Value && x.ItemLinkId == itemLink.Id)
            .FirstOrDefaultAsync();

        using var sesh = await db.Db.Client.StartSessionAsync();
        sesh.StartTransaction();

        var transactions = await plaid.TransactionsSyncAsync(new TransactionsSyncRequest
        {
            AccessToken = itemLink.PlaidAccessToken,
            Cursor = state?.Cursor != string.Empty ? state?.Cursor : null
        });

        Dictionary<string, ObjectId>? categoriesByName = null;
        if (transactions.Added.Count != 0)
        {
            var categoryNames = transactions.Added
                .Select(x => x.PersonalFinanceCategory?.Detailed)
                .Where(x => x is not null)
                .Cast<string>()
                .ToHashSet();
            categoryNames.Add(DefaultTransactionCategories.UnknownCategoryName);
            var categories = await db.TransactionCategories.FindAsync(
                Builders<TransactionCategory>.Filter.Eq(x => x.UserId, ctx.UserId!.Value) &
                Builders<TransactionCategory>.Filter.In(x => x.Name, categoryNames));

            categoriesByName = new Dictionary<string, ObjectId>();
            await categories.ForEachAsync(x => categoriesByName.Add(x.Name, x.Id));
        }

        var added = transactions.Added.Select(x => new InsertOneModel<Transaction>(new Transaction
        {
            ItemLinkId = itemLink.Id,
            PlaidTransaction = x,
            TransactionCategoryId = categoriesByName![x.PersonalFinanceCategory?.Detailed ?? "UNKNOWN"],
            UserId = ctx.UserId!.Value,
            InsertedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        }));

        var updated = transactions.Modified.Select(z =>
            new UpdateOneModel<Transaction>(
                Builders<Transaction>.Filter.Eq(y => y.PlaidTransaction.TransactionId, z.TransactionId),
                Builders<Transaction>.Update
                    .Set(x => x.PlaidTransaction, z)
                    .CurrentDate(x => x.UpdatedAt))
            {
                IsUpsert = true
            });

        var removed = transactions.Removed.Select(x =>
            new DeleteOneModel<Transaction>(Builders<Transaction>.Filter
                .Eq(y => y.PlaidTransaction.TransactionId, x.TransactionId)));

        var bulkOps = added.Cast<WriteModel<Transaction>>().Concat(updated).Concat(removed).ToArray();

        if (bulkOps.Length != 0)
        {
            await db.Transactions.BulkWriteAsync(bulkOps);
        }

        var nextCursor = transactions.NextCursor;

        if (state is null)
        {
            await db.TransactionSyncStates.InsertOneAsync(new TransactionsSyncState
            {
                UserId = ctx.UserId!.Value,
                ItemLinkId = itemLink.Id,
                Cursor = nextCursor,
                InsertedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
        else
        {
            var filter = Builders<TransactionsSyncState>.Filter.Empty;
            filter &= Builders<TransactionsSyncState>.Filter.Eq(x => x.UserId, ctx.UserId!.Value);
            filter &= Builders<TransactionsSyncState>.Filter.Eq(x => x.ItemLinkId, itemLink.Id);
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