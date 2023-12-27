using Going.Plaid;
using Going.Plaid.Item;
using Graph.Domain.Entities.Items;
using Graph.Infrastructure;
using HotChocolate.Authorization;
using HotChocolate.Language;
using MongoDB.Driver;

namespace Graph.GraphQL.Mutations.Items;

[ExtendObjectType(OperationType.Mutation)]
public class CreateItemLinkMutation
{
    [GraphQLName(nameof(CreateItemLink) + nameof(Request))]
    public class Request
    {
        public string PublicToken { get; set; }
    }

    [Authorize]
    public async Task<ItemLink> CreateItemLink(Request request, UserContext ctx, PlaidClient plaid,
        SpendDb db)
    {
        var res = await plaid.ItemPublicTokenExchangeAsync(new ItemPublicTokenExchangeRequest
        {
            PublicToken = request.PublicToken
        });
        var item = new ItemLink
        {
            ItemId = res.ItemId,
            UserId = ctx.UserId!.Value,
            AccessToken = res.AccessToken,
            InsertedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await db.ItemLinks.InsertOneAsync(item, new InsertOneOptions());

        return item;
    }
}