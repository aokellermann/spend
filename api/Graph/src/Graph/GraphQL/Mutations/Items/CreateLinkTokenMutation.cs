using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Link;
using HotChocolate.Language;
using Spend.Graph.Infrastructure;
using Spend.Graph.Infrastructure.Plaid;

namespace Spend.Graph.GraphQL.Mutations.Items;

[ExtendObjectType(OperationType.Mutation)]
public class CreateLinkTokenMutation
{
    /// <summary>
    ///     Create link token response.
    /// </summary>
    [GraphQLName(nameof(CreateLinkToken) + nameof(Response))]
    public class Response
    {
        /// <summary>
        ///     A link_token is a token used to initialize Link, and must be provided any time you are presenting your
        ///     user with the Link interface. You can obtain a Link token by calling /link/token/create. For more
        ///     details, see the the Token exchange flow. A link_token expires after 4 hours (or after 30 minutes,
        ///     when being used with update mode).
        /// </summary>
        public string LinkToken { get; init; } = default!;
    }

    /// <summary>
    ///     Creates a link token. See https://plaid.com/docs/link/.
    /// </summary>
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