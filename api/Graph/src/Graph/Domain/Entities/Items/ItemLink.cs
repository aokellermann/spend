using Going.Plaid;
using Going.Plaid.Accounts;
using Going.Plaid.Auth;
using Going.Plaid.Entity;
using Going.Plaid.Identity;
using Going.Plaid.Investments;
using Going.Plaid.Item;
using Going.Plaid.Liabilities;
using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Infrastructure;
using Transaction = Spend.Graph.Domain.Entities.Transactions.Transaction;

namespace Spend.Graph.Domain.Entities.Items;

/// <summary>
///     Stores user information for a Plaid Item.<para/>
///
///     An item is an authorized connection to one of a user's financial institution account(s).
///     An item may provide access to multiple accounts.
/// </summary>
public class ItemLink : AuditableEntityWithTenantBase<ObjectId>
{
    /// <summary>
    ///     The Plaid Item ID. The item_id is always unique; linking the same account at the same institution twice will
    ///     result in two Items with different item_id values. Like all Plaid identifiers, the item_id is
    ///     case-sensitive.
    /// </summary>
    public string PlaidItemId { get; init; } = default!;

    /// <summary>
    ///     An access_token is a token used to make API requests related to a specific Item. You will typically obtain
    ///     an access_token by calling /item/public_token/exchange. For more details, see the Token exchange flow. An
    ///     access_token does not expire, although it may require updating, such as when a user changes their password,
    ///     or when working with European institutions that comply with PSD2's 90-day consent window. For more
    ///     information, see When to use update mode. Access tokens should always be stored securely, and associated
    ///     with the user whose data they represent. If compromised, an access_token can be rotated via
    ///     /item/access_token/invalidate. If no longer needed, it can be revoked via /item/remove.
    /// </summary>
    [GraphQLIgnore]
    public string PlaidAccessToken { get; init; } = default!;

    /// <summary>
    ///     Transactions associated with this item.
    /// </summary>
    public IExecutable<Transaction> GetTransactions(SpendDb db)
        => db.Transactions.Find(x => x.ItemLinkId == Id).AsExecutable();

    /// <summary>
    ///    Item information. See https://plaid.com/docs/api/items/#itemget".
    /// </summary>
    public Task<ItemGetResponse> GetPlaidItem(PlaidClient client)
    {
        return client.ItemGetAsync(new ItemGetRequest { AccessToken = PlaidAccessToken });
    }

    /// <summary>
    ///     Account information. See https://plaid.com/docs/api/accounts/#accountsget.<para/>
    ///
    ///     The /accounts/get endpoint can be used to retrieve a list of accounts associated with any linked Item.
    ///     Plaid will only return active bank accounts — that is, accounts that are not closed and are capable of
    ///     carrying a balance. For items that went through the updated account selection pane, this endpoint only
    ///     returns accounts that were permissioned by the user when they initially created the Item. If a user creates
    ///     a new account after the initial link, you can capture this event through the NEW_ACCOUNTS_AVAILABLE webhook
    ///     and then use Link's update mode to request that the user share this new account with you.<para/>
    ///
    ///     /accounts/get is free to use and retrieves cached information, rather than extracting fresh information
    ///     from the institution. The balance returned will reflect the balance at the time of the last successful Item
    ///     update. If the Item is enabled for a regularly updating product, such as Transactions, Investments, or
    ///     Liabilities, the balance will typically update about once a day, as long as the Item is healthy. If the
    ///     Item is enabled only for products that do not frequently update, such as Auth or Identity, balance data may
    ///     be much older.
    ///
    ///     For realtime balance information, use the paid endpoint /accounts/balance/get instead.
    /// </summary>
    public Task<AccountsGetResponse> GetPlaidAccounts(PlaidClient client, AccountsGetRequestOptions? options = null)
    {
        return client.AccountsGetAsync(new AccountsGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }

    /// <summary>
    ///     Auth information. See https://plaid.com/docs/api/products/auth/#authget.<para/>
    ///
    ///     The /auth/get endpoint returns the bank account and bank identification numbers (such as routing numbers,
    ///     for US accounts) associated with an Item's checking and savings accounts, along with high-level account
    ///     data and balances when available.<para/>
    ///
    ///     Note: This request may take some time to complete if auth was not specified as an initial product when
    ///     creating the Item. This is because Plaid must communicate directly with the institution to retrieve the
    ///     data.
    /// </summary>
    public Task<AuthGetResponse> GetPlaidAuth(PlaidClient client, AuthGetRequestOptions? options = null)
    {
        return client.AuthGetAsync(new AuthGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }

    /// <summary>
    ///     Balance information. See https://plaid.com/docs/api/products/balance/#accountsbalanceget.
    /// </summary>
    /// <remarks>
    ///     The /accounts/balance/get endpoint returns the real-time balance for each of an Item's accounts. While other
    ///     endpoints, such as /accounts/get, return a balance object, only /accounts/balance/get forces the available
    ///     and current balance fields to be refreshed rather than cached. This endpoint can be used for existing Items
    ///     that were added via any of Plaid’s other products. This endpoint can be used as long as Link has been
    ///     initialized with any other product, balance itself is not a product that can be used to initialize Link. As
    ///     this endpoint triggers a synchronous request for fresh data, latency may be higher than for other Plaid
    ///     endpoints (typically less than 10 seconds, but occasionally up to 30 seconds or more); if you encounter
    ///     errors, you may find it necessary to adjust your timeout period when making requests.
    /// </remarks>
    public Task<AccountsGetResponse> GetPlaidBalance(PlaidClient client, AccountsBalanceGetRequestOptions? options = null)
    {
        return client.AccountsBalanceGetAsync(new AccountsBalanceGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }

    /// <summary>
    ///     Identity information. See https://plaid.com/docs/api/products/identity/#identityget.<para/>
    ///
    ///     The /identity/get endpoint allows you to retrieve various account holder information on file with the
    ///     financial institution, including names, emails, phone numbers, and addresses. Only name data is guaranteed
    ///     to be returned; other fields will be empty arrays if not provided by the institution.<para/>
    ///
    ///     This request may take some time to complete if identity was not specified as an initial product when
    ///     creating the Item. This is because Plaid must communicate directly with the institution to retrieve the
    ///     data.
    /// </summary>
    public Task<IdentityGetResponse> GetPlaidIdentity(PlaidClient client, IdentityGetRequestOptions? options = null)
    {
        return client.IdentityGetAsync(new IdentityGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }

    /// <summary>
    ///     Liabilities information. See https://plaid.com/docs/api/products/liabilities/#liabilitiesget.<para/>
    ///
    ///     The /liabilities/get endpoint returns various details about an Item with loan or credit accounts.
    ///     Liabilities data is available primarily for US financial institutions, with some limited coverage of
    ///     Canadian institutions. Currently supported account types are account type credit with account subtype
    ///     credit card or paypal, and account type loan with account subtype student or mortgage. To limit accounts
    ///     listed in Link to types and subtypes supported by Liabilities, you can use the account_filters parameter
    ///     when creating a Link token.<para/>
    ///
    ///     The types of information returned by Liabilities can include balances and due dates, loan terms, and
    ///     account details such as original loan amount and guarantor. Data is refreshed approximately once per day;
    ///     the latest data can be retrieved by calling /liabilities/get.<para/>
    ///
    ///     Note: This request may take some time to complete if liabilities was not specified as an initial product
    ///     when creating the Item. This is because Plaid must communicate directly with the institution to retrieve
    ///     the additional data.
    /// </summary>
    public Task<LiabilitiesGetResponse> GetPlaidLiabilities(PlaidClient client, LiabilitiesGetRequestOptions? options = null)
    {
        return client.LiabilitiesGetAsync(new LiabilitiesGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }

    /// <summary>
    ///     Investment holdings information. See https://plaid.com/docs/api/products/investments/#investmentsholdingsget. <para/>
    ///
    ///     The /investments/holdings/get endpoint allows developers to receive user-authorized stock position data for
    ///     investment-type accounts.
    /// </summary>
    public Task<InvestmentsHoldingsGetResponse> GetPlaidInvestmentHoldings(PlaidClient client,
        InvestmentHoldingsGetRequestOptions? options = null)
    {
        return client.InvestmentsHoldingsGetAsync(new InvestmentsHoldingsGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }


    /// <summary>
    ///     Investment transaction information. See https://plaid.com/docs/api/products/investments/#investmentstransactionsget.<para/>
    ///
    ///     The /investments/transactions/get endpoint allows developers to retrieve up to 24 months of user-authorized
    ///     transaction data for investment accounts.<para/>
    ///
    ///     Transactions are returned in reverse-chronological order, and the sequence of transaction ordering is stable
    ///     and will not shift.<para/>
    ///
    ///     Due to the potentially large number of investment transactions associated with an Item, results are
    ///     paginated. Manipulate the count and offset parameters in conjunction with the total_investment_transactions
    ///     response body field to fetch all available investment transactions.<para/>
    ///
    ///     Note that Investments does not have a webhook to indicate when initial transaction data has loaded (unless
    ///     you use the async_update option). Instead, if transactions data is not ready when
    ///     /investments/transactions/get is first called, Plaid will wait for the data. For this reason, calling
    ///     /investments/transactions/get immediately after Link may take up to one to two minutes to return.<para/>
    ///
    ///     Data returned by the asynchronous investments extraction flow (when async_update is set to true) may not be
    ///     immediately available to /investments/transactions/get. To be alerted when the data is ready to be fetched,
    ///     listen for the HISTORICAL_UPDATE webhook. If no investments history is ready when
    ///     /investments/transactions/get is called, it will return a PRODUCT_NOT_READY error.
    /// </summary>
    public Task<InvestmentsTransactionsGetResponse> GetPlaidInvestmentTransactions(PlaidClient client,
        InvestmentsTransactionsGetRequestOptions? options = null)
    {
        return client.InvestmentsTransactionsGetAsync(new InvestmentsTransactionsGetRequest
        {
            AccessToken = PlaidAccessToken, Options = options
        });
    }
}