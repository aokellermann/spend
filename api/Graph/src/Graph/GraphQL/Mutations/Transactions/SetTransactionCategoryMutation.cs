using HotChocolate.Language;
using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Domain.Errors;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.Transactions;

[ExtendObjectType(OperationType.Mutation)]
public class SetTransactionCategoryMutation
{
    /// <summary>
    ///     Updates the transaction category.
    /// </summary>
    /// <param name="transactionId">The transaction to update.</param>
    /// <param name="transactionVersion">The transaction version.</param>
    /// <param name="transactionCategoryId">The transaction category to update to.</param>
    /// <param name="transactionCategoryVersion">The transaction category version.</param>
    /// <param name="ctx"></param>
    /// <param name="db"></param>
    [Error(typeof(NotFoundException))]
    public async Task<Transaction?> SetTransactionCategory(
        ObjectId transactionId,
        long transactionVersion,
        ObjectId transactionCategoryId,
        long transactionCategoryVersion,
        UserContext ctx,
        SpendDb db
    )
    {
        using var sesh = await db.Db.Client.StartSessionAsync();
        sesh.StartTransaction();

        await db.TransactionCategories.FindRequiredAuditable(transactionCategoryId, transactionCategoryVersion, ctx);

        var res = await db.Transactions.FindOneAndUpdateAsync(
            db.Transactions.AuditableFilter(transactionId, transactionVersion, ctx),
            Builders<Transaction>.Update
                .Set(x => x.TransactionCategoryId, transactionCategoryId)
                .Audit(),
            new FindOneAndUpdateOptions<Transaction>
            {
                ReturnDocument = ReturnDocument.After
            });
        if (res is null)
        {
            throw new NotFoundException(typeof(Transaction), transactionId);
        }

        await sesh.CommitTransactionAsync();
        return res;
    }
}