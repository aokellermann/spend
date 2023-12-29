using HotChocolate.Language;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Queries.Transactions;

/// <summary>
///     Get transactions query.
/// </summary>
[ExtendObjectType(OperationType.Query)]
public class GetTransactionsQuery
{
    /// <summary>
    ///     Get transactions.
    /// </summary>
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<Transaction> GetTransactions(UserContext ctx, SpendDb db)
        => db.Transactions.Find(x => x.UserId == ctx.UserId!.Value).AsExecutable();
}