using HotChocolate.Language;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Queries.Items;

/// <summary>
///     Get item links query.
/// </summary>
[ExtendObjectType(OperationType.Query)]
public class GetItemLinksQuery
{
    /// <summary>
    ///     Get item links.
    /// </summary>
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<ItemLink> GetItemLinks(UserContext ctx, SpendDb db)
        => db.ItemLinks.Find(x => x.UserId == ctx.UserId!.Value).AsExecutable();
}