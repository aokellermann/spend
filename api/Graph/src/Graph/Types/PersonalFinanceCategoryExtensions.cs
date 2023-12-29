using Going.Plaid.Entity;
using Spend.Graph.Domain.Entities.Transactions;

namespace Spend.Graph.Types;

[ExtendObjectType(typeof(PersonalFinanceCategory))]
public class PersonalFinanceCategoryExtensions
{
    public string GetDescription([Parent] PersonalFinanceCategory category)
        => DefaultTransactionCategories.ByName[category.Detailed].Description;
}