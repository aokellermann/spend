using HotChocolate.Language;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.User;

/// <summary>
///     Initialize user mutation.
/// </summary>
[ExtendObjectType(OperationType.Mutation)]
public class InitializeUserMutation
{
    [GraphQLName(nameof(InitializeUser) + nameof(Response))]
    public class Response
    {
        public IEnumerable<TransactionCategory> InitializedTransactionCategories { get; set; } =
            Enumerable.Empty<TransactionCategory>();
    }

    /// <summary>
    ///     Initializes user data. This mutation is idempotent.
    /// </summary>
    public async Task<Response> InitializeUser(UserContext ctx, SpendDb db)
    {
        var res = new Response();

        using var sesh = await db.Db.Client.StartSessionAsync();
        if (await db.TransactionCategories.CountDocumentsAsync(
                Builders<TransactionCategory>.Filter.Eq(x => x.UserId, ctx.UserId!.Value)) == 0)
        {
            sesh.StartTransaction();

            var parentDocuments = DefaultTransactionCategories.AsList
                .Select(x => new TransactionCategory
                {
                    InsertedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = ctx.UserId!.Value,
                    Name = x.Name,
                }).ToList();
            await db.TransactionCategories.InsertManyAsync(parentDocuments);

            var parentIdsByName = parentDocuments.ToDictionary(x => x.Name, x => x.Id);
            var childDocuments = DefaultTransactionCategories.AsList
                .SelectMany(x => x.ChildDefaultTransactionCategories!)
                .Select(y => new TransactionCategory()
                {
                    InsertedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = ctx.UserId!.Value,
                    Name = y.Name,
                    Description = y.Description,
                    ParentTransactionCategoryId = parentIdsByName[y.ParentTransactionCategoryName!]
                }).ToList();

            await db.TransactionCategories.InsertManyAsync(childDocuments);

            await sesh.CommitTransactionAsync();

            res.InitializedTransactionCategories = parentDocuments.Concat(childDocuments);
        }

        return res;
    }
}