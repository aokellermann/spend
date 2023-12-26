using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Link;
using Graph.GraphQL.Mutations.Item;
using Graph.Infrastructure;
using Graph.Infrastructure.Plaid;
using HotChocolate.Authorization;
using HotChocolate.Language;

namespace Graph.GraphQL.Mutations.Plaid;

[ExtendObjectType(OperationType.Mutation)]
public class CreateLinkTokenMutation
{
    [GraphQLName(nameof(CreateLinkToken) + nameof(Response))]
    public class Response
    {
        public string LinkToken { get; set; }
    }

    [Authorize]
    public async Task<Response> CreateLinkToken(UserContext ctx, PlaidClient plaid)
    {
        var res = await plaid.LinkTokenCreateAsync(new LinkTokenCreateRequest
        {
            ClientName = PlaidConstants.ClientName,
            AndroidPackageName = PlaidConstants.AndroidPackageName,
            Language = Language.English,
            CountryCodes = new[] { CountryCode.Us },
            User = new LinkTokenCreateRequestUser
            {
                ClientUserId = ctx.UserId.ToString()!,
            },
            Products = new[] { Products.Auth, Products.Transactions },
        });

        return new Response
        {
            LinkToken = res.LinkToken
        };
    }
}