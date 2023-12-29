using HotChocolate.Language;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Transactions;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Queries.Transactions;

/// <summary>
///     Get transaction sync state query.
/// </summary>
[ExtendObjectType(OperationType.Query)]
public class GetTransactionsSyncStatesQuery
{
    /// <summary>
    ///     Get transaction sync states.
    /// </summary>
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<TransactionsSyncState> GetTransactionsSyncStates(UserContext ctx, SpendDb db)
        => db.TransactionSyncStates.Find(x => x.UserId == ctx.UserId!.Value).AsExecutable();
}