using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Institutions;

namespace Spend.Graph.Types;

/// <summary>
///     Type extensions for item.
/// </summary>
public class ItemCodeFirstTypeExtensions : ObjectTypeExtension<Item>
{
    /// <summary>
    ///     Configures type extensions for item.
    /// </summary>
    /// <param name="descriptor"></param>
    protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
    {
        descriptor.Ignore(x => x.Webhook);
    }
}

/// <summary>
///     Type extensions for item.
/// </summary>
[ExtendObjectType(typeof(Item))]
public class ItemAnnotationBasedTypeExtensions
{
    /// <summary>
    ///     Get institution associated with an item. See https://plaid.com/docs/api/institutions/#institutionsget_by_id.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="client"></param>
    /// <param name="countryCodes">Specify which country or countries to include institutions from, using the ISO-3166-1 alpha-2 country code standard. In API versions 2019-05-29 and earlier, the <c>country_codes</c> parameter is an optional parameter within the <c>options</c> object and will default to <c>[US]</c> if it is not supplied.</param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<InstitutionsGetByIdResponse?> GetInstitution(
        [Parent] Item item, PlaidClient client, CountryCode[]? countryCodes = null,
        InstitutionsGetByIdRequestOptions? options = null)
    {
        if (item.InstitutionId is null) return null;

        return await client.InstitutionsGetByIdAsync(new InstitutionsGetByIdRequest
        {
            InstitutionId = item.InstitutionId,
            CountryCodes = countryCodes!,
            Options = options
        });
    }
}