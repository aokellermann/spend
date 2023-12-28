using Going.Plaid;

namespace Spend.Graph.Types;

/// <summary>
///     Plaid reponse base extensions.
/// </summary>
public class ResponseBaseExtensions : ObjectTypeExtension<ResponseBase>
{
    /// <summary>
    ///     Removes raw json field.
    /// </summary>
    protected override void Configure(IObjectTypeDescriptor<ResponseBase> descriptor)
    {
        // todo: not working??
        descriptor.Ignore(x => x.RawJson);
    }
}