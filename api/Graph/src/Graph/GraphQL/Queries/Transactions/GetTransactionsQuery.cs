using Graph.Domain.Entities.Transactions;
using Graph.Infrastructure;
using HotChocolate.Authorization;
using HotChocolate.Language;
using MongoDB.Driver;

namespace Graph.GraphQL.Queries.Transactions;

[ExtendObjectType(OperationType.Query)]
public class GetTransactionsQuery
{
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    [Authorize]
    public IExecutable<Transaction> GetTransactions(UserContext ctx, SpendDb db)
        => db.Transactions.Find(x => x.UserId == ctx.UserId!.Value).AsExecutable();
}