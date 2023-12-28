using Going.Plaid;
using Going.Plaid.Item;
using HotChocolate.Authorization;
using HotChocolate.Language;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.GraphQL.Mutations.Items;

/// <summary>
///     Create item link mutation.
/// </summary>
[ExtendObjectType(OperationType.Mutation)]
public class CreateItemLinkMutation
{
    /// <summary>
    ///     Create item link mutation request.
    /// </summary>
    [GraphQLName(nameof(CreateItemLink) + nameof(Request))]
    public class Request
    {
        /// <summary>
        ///     A public_token is a token obtained from Link's onSuccess callback. This token can be exchanged for an
        ///     access_token by calling /item/public_token/exchange. For more details, see the Token exchange flow. A
        ///     public_token expires after 30 minutes.
        /// </summary>
        public string PublicToken { get; init; } = default!;
    }

    /// <summary>
    ///     Creates an <see cref="ItemLink"/> from a public token. See https://plaid.com/docs/link/.
    /// </summary>
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
            PlaidItemId = res.ItemId,
            UserId = ctx.UserId!.Value,
            PlaidAccessToken = res.AccessToken,
            InsertedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await db.ItemLinks.InsertOneAsync(item, new InsertOneOptions());

        return item;
    }
}