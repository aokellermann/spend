using HotChocolate.Language;
using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Domain.Errors;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.Transactions.Categories;

[ExtendObjectType(OperationType.Mutation)]
public class UpdateTransactionCategoryMutation
{
    /// <summary>
    ///     Update a transaction category.
    /// </summary>
    /// <param name="id">Category id.</param>
    /// <param name="version">Version.</param>
    /// <param name="name">Category name. 64 characters max.</param>
    /// <param name="description">Category description. 256 characters max.</param>
    /// <param name="parent">Parent id if any.</param>
    /// <param name="parentVersion">Parent version if any.</param>
    /// <param name="ctx"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Error(typeof(PreconditionFailedException))]
    public async Task<TransactionCategory> UpdateTransactionCategory(ObjectId id, long version, string name,
        string? description, ObjectId? parent,
        long? parentVersion, UserContext ctx, SpendDb db)
    {
        if (name.Length is 0 or > 64)
            throw new PreconditionFailedException(typeof(TransactionCategory),
                "name must be between 1 and 64 characters");

        if (description?.Length == 0) description = null;
        else if (description?.Length > 256)
            throw new PreconditionFailedException(typeof(TransactionCategory),
                "description must be less than 256 characters");

        if (parent is null ^ parentVersion is null)
            throw new PreconditionFailedException(typeof(TransactionCategory),
                "parent and parentVersion must both be null or not null");

        var sesh = await db.Db.Client.StartSessionAsync();
        sesh.StartTransaction();

        if (parent != null)
        {
            var parentCategory =
                await db.TransactionCategories.FindRequiredAuditable(parent.Value, parentVersion!.Value, ctx);
            if (parentCategory.ParentTransactionCategoryId != null)
            {
                throw new PreconditionFailedException(typeof(TransactionCategory),
                    "only one level of category nesting allowed");
            }
        }

        var withSameName = await (await db.TransactionCategories.FindAsync(
            db.TransactionCategories.TenantFilter(ctx) &
            Builders<TransactionCategory>.Filter.Eq(x => x.Name, name)
        )).FirstOrDefaultAsync();
        if (withSameName is not null && withSameName.Id != id)
        {
            throw new ConflictException(typeof(TransactionCategory), name);
        }

        var res = await db.TransactionCategories.FindOneAndUpdateAsync(
            db.TransactionCategories.AuditableFilter(id, version, ctx),
            Builders<TransactionCategory>.Update
                .Set(x => x.Name, name)
                .Set(x => x.Description, description)
                .Set(x => x.ParentTransactionCategoryId, parent)
                .Audit(), new FindOneAndUpdateOptions<TransactionCategory>()
            {
                ReturnDocument = ReturnDocument.After
            });

        await sesh.CommitTransactionAsync();

        return res;
    }
}