using Graph.Domain.Entities.Items;
using Graph.Infrastructure;
using HotChocolate.Authorization;
using HotChocolate.Language;
using MongoDB.Driver;

namespace Graph.GraphQL.Queries.Items;

[ExtendObjectType(OperationType.Query)]
public class GetItemLinksQuery
{
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    [Authorize]
    public IExecutable<ItemLink> GetItemLinks(UserContext ctx, SpendDb db)
        => db.ItemLinks.Find(x => x.UserId == ctx.UserId!.Value).AsExecutable();
}