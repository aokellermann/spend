using Going.Plaid;
using Going.Plaid.Item;
using Graph.Infrastructure;
using HotChocolate.Authorization;
using HotChocolate.Language;

namespace Graph.GraphQL.Mutations.Item;

[ExtendObjectType(OperationType.Mutation)]
public class CreateItemLinkMutation
{
    [GraphQLName(nameof(CreateItemLink) + nameof(Request))]
    public class Request
    {
        public string PublicToken { get; set; }
    }

    [Authorize]
    public async Task<Domain.Entities.ItemLink> CreateItemLink(Request request, UserContext ctx, PlaidClient plaid, SpendDbContext db)
    {
        var res = await plaid.ItemPublicTokenExchangeAsync(new ItemPublicTokenExchangeRequest
        {
            PublicToken = request.PublicToken
        });

        var entity = await db.ItemLink.AddAsync(new Domain.Entities.ItemLink
        {
            ItemId = res.ItemId,
            UserId = ctx.UserId!.Value,
            AccessToken = res.AccessToken,
            InsertedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync();

        return entity.Entity;
    }
}