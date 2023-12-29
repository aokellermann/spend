using HotChocolate.Language;
using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Domain.Errors;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.Transactions.Transactions;

[ExtendObjectType(OperationType.Mutation)]
public class SetTransactionCategoriesMutation
{
    /// <summary>
    ///     Set transaction categories transaction request.
    /// </summary>
    [GraphQLName(nameof(SetTransactionCategories) + nameof(TransactionRequest))]
    public class TransactionRequest
    {
        /// <summary>
        ///     Transaction id.
        /// </summary>
        public ObjectId Id { get; init; }

        /// <summary>
        ///     Transaction version.
        /// </summary>
        public long Version { get; init; }
    }

    /// <summary>
    ///     Set transaction categories response.
    /// </summary>
    [GraphQLName(nameof(SetTransactionCategories) + nameof(Response))]
    public class Response
    {
        /// <summary>
        ///     The number of records that were updated.
        /// </summary>
        public long UpdatedCount { get; init; }
    }

    /// <summary>
    ///     Updates transaction category for multiple transactions.
    /// </summary>
    /// <param name="transactions">The transaction version.</param>
    /// <param name="transactionCategoryId">The transaction category to update to.</param>
    /// <param name="transactionCategoryVersion">The transaction category version.</param>
    /// <param name="ctx"></param>
    /// <param name="db"></param>
    [Error(typeof(NotFoundException))]
    [Error(typeof(PreconditionFailedException))]
    public async Task<Response> SetTransactionCategories(
        TransactionRequest[] transactions,
        ObjectId transactionCategoryId,
        long transactionCategoryVersion,
        UserContext ctx,
        SpendDb db
    )
    {
        if (transactions.Select(x => x.Id).Distinct().Count() != transactions.Length)
        {
            throw new PreconditionFailedException(typeof(Transaction),
                "duplicate transaction id");
        }

        using var sesh = await db.Db.Client.StartSessionAsync();
        sesh.StartTransaction();

        await db.TransactionCategories.FindRequiredAuditable(transactionCategoryId, transactionCategoryVersion, ctx);

        var updateRequests = transactions.Select(x =>
            new UpdateOneModel<Transaction>(
                db.Transactions.AuditableFilter(x.Id, x.Version, ctx),
                Builders<Transaction>.Update
                    .Set(y => y.TransactionCategoryId, transactionCategoryId)
                    .Audit()));

        var res = await db.Transactions.BulkWriteAsync(updateRequests);

        await sesh.CommitTransactionAsync();
        return new Response
        {
            UpdatedCount = res.ModifiedCount
        };
    }
}