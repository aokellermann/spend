using HotChocolate.Language;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Queries.Transactions;

/// <summary>
///     Get transaction categories query.
/// </summary>
[ExtendObjectType(OperationType.Query)]
public class GetTransactionCategoriesQuery
{
    /// <summary>
    ///     Get transactions.
    /// </summary>
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<TransactionCategory> GetTransactionCategories(UserContext ctx, SpendDb db)
        => db.TransactionCategories.Find(x => x.UserId == ctx.UserId!.Value && x.ParentTransactionCategoryId == null).AsExecutable();
}