using Going.Plaid;

namespace Graph.Types;

public class ResponseBaseExtensions : ObjectTypeExtension<ResponseBase>
{
    protected override void Configure(IObjectTypeDescriptor<ResponseBase> descriptor)
    {
        // todo: not working??
        descriptor.Ignore(x => x.RawJson);
    }
}