using System.Reflection;
using Going.Plaid;
using Going.Plaid.Entity;
using HotChocolate.Configuration;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Types.Descriptors.Definitions;

namespace Graph.Types;

/// <summary>
///     Renames all plaid types to $"Plaid{Name}" to eliminate type conflicts.
/// </summary>
public class PlaidTypeInterceptor : TypeInterceptor
{
    private static readonly Assembly PlaidAssembly = Assembly.GetAssembly(typeof(PlaidClient))!;
    private static readonly Assembly HotChocolateAssembly = Assembly.GetAssembly(typeof(SortInputType))!;


    public override void OnBeforeCompleteName(ITypeCompletionContext completionContext, DefinitionBase definition)
    {
        var type = definition switch
        {
            ObjectTypeDefinition objectTypeDefinition => objectTypeDefinition.FieldBindingType,
            EnumTypeDefinition enumTypeDefinition => enumTypeDefinition.RuntimeType,
            SortInputTypeDefinition sortInputTypeDefinition => sortInputTypeDefinition.EntityType,
            FilterInputTypeDefinition filterInputTypeDefinition => filterInputTypeDefinition.EntityType,
            _ => null
        };

        if (type is null) return;
        if (type.Assembly == PlaidAssembly ||
            (type.Assembly == HotChocolateAssembly &&
             type.GenericTypeArguments.FirstOrDefault()?.Assembly == PlaidAssembly))
        {
            definition.Name = "Plaid" + definition.Name;
        }
    }
}