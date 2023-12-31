using HotChocolate.Language;
using Spend.Graph.Domain.Entities.Transactions;

namespace Spend.Graph.GraphQL.Queries.Transactions;

/// <summary>
///     Get default transaction categories query.
/// </summary>
[ExtendObjectType(OperationType.Query)]
public class GetDefaultTransactionCategoriesQuery
{
    /// <summary>
    ///     Gets the default transaction categories.
    /// </summary>
    public IReadOnlyList<DefaultTransactionCategory> GetDefaultTransactionCategories()
        => DefaultTransactionCategories.AsList;
}