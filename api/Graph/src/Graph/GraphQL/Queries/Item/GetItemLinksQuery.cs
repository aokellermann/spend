using Graph.Infrastructure;
using HotChocolate.Language;

namespace Graph.GraphQL.Queries.Item;

[ExtendObjectType(OperationType.Query)]
public class GetItemLinksQuery
{
    public IQueryable<Domain.Entities.ItemLink> GetItemLinks(UserContext ctx, SpendDbContext db)
    {
        return db.ItemLink.Where(x => x.UserId == ctx.UserId!);
    }
}