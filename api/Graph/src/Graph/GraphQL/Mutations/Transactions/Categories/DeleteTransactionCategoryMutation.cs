using HotChocolate.Language;
using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Domain.Errors;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.Transactions.Categories;

[ExtendObjectType(OperationType.Mutation)]
public class DeleteTransactionCategoryMutation
{
    /// <summary>
    ///     Delete transaction category
    /// </summary>
    /// <param name="id">Transaction category id.</param>
    /// <param name="version">Transaction category version.</param>
    [Error(typeof(NotFoundException))]
    [Error(typeof(PreconditionFailedException))]
    public async Task<DeleteResult> DeleteTransactionCategory(
        ObjectId id, long version, UserContext ctx, SpendDb db)
    {
        var sesh = await db.Db.Client.StartSessionAsync();
        sesh.StartTransaction();

        var transactionCount = await db.Transactions.CountDocumentsAsync(
            db.Transactions.TenantFilter(ctx) &
            Builders<Transaction>.Filter.Eq(x => x.TransactionCategoryId, id));

        if (transactionCount != 0)
        {
            throw new PreconditionFailedException(typeof(TransactionCategory),
                $"there are {transactionCount} transactions that use this category");
        }

        var childrenCount = await db.TransactionCategories.CountDocumentsAsync(
            db.TransactionCategories.TenantFilter(ctx) &
            Builders<TransactionCategory>.Filter.Eq(x => x.ParentTransactionCategoryId, id));

        if (childrenCount != 0)
        {
            throw new PreconditionFailedException(typeof(TransactionCategory),
                $"there are {childrenCount} child transaction categories");
        }

        var category = await db.TransactionCategories.FindRequiredAuditable(id, version, ctx);
        if (category.Name == DefaultTransactionCategories.UnknownCategoryName)
        {
            throw new PreconditionFailedException(typeof(TransactionCategory),
                $"{DefaultTransactionCategories.UnknownCategoryName} category cannot be deleted");
        }

        var res = await db.TransactionCategories.DeleteOneAsync(
            db.TransactionCategories.AuditableFilter(id, version, ctx));

        await sesh.CommitTransactionAsync();

        return res;
    }
}