using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Institutions;

namespace Graph.Types;

public class ItemCodeFirstTypeExtensions : ObjectTypeExtension<Item>
{
    protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
    {
        descriptor.Ignore(x => x.Webhook);
    }
}

[ExtendObjectType(typeof(Item))]
public class ItemAnnotationBasedTypeExtensions
{
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