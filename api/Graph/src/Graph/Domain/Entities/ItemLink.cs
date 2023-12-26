using System.ComponentModel.DataAnnotations;
using Going.Plaid;
using Going.Plaid.Accounts;
using Going.Plaid.Auth;
using Going.Plaid.Entity;
using Going.Plaid.Identity;
using Going.Plaid.Investments;
using Going.Plaid.Item;
using Going.Plaid.Liabilities;
using Microsoft.EntityFrameworkCore;

namespace Graph.Domain.Entities;

[Index(nameof(UserId))]
public class ItemLink
{
    [Key]
    public long Id { get; set; }

    public Guid UserId { get; set; }

    [Unicode(false)]
    [MaxLength(64)]
    public string ItemId { get; set; }

    [Unicode(false)]
    [MaxLength(64)]
    [GraphQLIgnore]
    public string AccessToken { get; set; }

    public DateTime InsertedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Task<ItemGetResponse> GetItem(PlaidClient client)
    {
        return client.ItemGetAsync(new ItemGetRequest { AccessToken = AccessToken });
    }

    public Task<AccountsGetResponse> GetAccounts(PlaidClient client, AccountsGetRequestOptions? options = null)
    {
        return client.AccountsGetAsync(new AccountsGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }

    public Task<AuthGetResponse> GetAuth(PlaidClient client, AuthGetRequestOptions? options = null)
    {
        return client.AuthGetAsync(new AuthGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }

    public Task<AccountsGetResponse> GetBalance(PlaidClient client, AccountsBalanceGetRequestOptions? options = null)
    {
        return client.AccountsBalanceGetAsync(new AccountsBalanceGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }

    public Task<IdentityGetResponse> GetIdentity(PlaidClient client, IdentityGetRequestOptions? options = null)
    {
        return client.IdentityGetAsync(new IdentityGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }

    public Task<LiabilitiesGetResponse> GetLiabilities(PlaidClient client, LiabilitiesGetRequestOptions? options = null)
    {
        return client.LiabilitiesGetAsync(new LiabilitiesGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }

    public Task<InvestmentsHoldingsGetResponse> GetInvestmentHoldings(PlaidClient client,
        InvestmentHoldingsGetRequestOptions? options = null)
    {
        return client.InvestmentsHoldingsGetAsync(new InvestmentsHoldingsGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }


    public Task<InvestmentsTransactionsGetResponse> GetInvestmentTransactions(PlaidClient client,
        InvestmentsTransactionsGetRequestOptions? options = null)
    {
        return client.InvestmentsTransactionsGetAsync(new InvestmentsTransactionsGetRequest
        {
            AccessToken = AccessToken, Options = options
        });
    }
}