namespace Spend.Graph.Domain.Entities.Transactions;

/// <summary>
///     Default transaction category. Plaid transactions are tagged with these. Plaid category data is obtained from
///     https://plaid.com/documents/transactions-personal-finance-category-taxonomy.csv.
/// </summary>
public class DefaultTransactionCategory
{
    public DefaultTransactionCategory(string name, string? description, string? parentTransactionCategoryName, DefaultTransactionCategory[]? children)
    {
        Name = name;
        Description = description;
        if (parentTransactionCategoryName != null)
            ParentTransactionCategoryName = string.Intern(parentTransactionCategoryName);
        ChildDefaultTransactionCategories = children;
    }

    /// <summary>
    ///     Unique (per-user) category name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     User-friendly description.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    ///     If a child category, the parent category name.
    /// </summary>
    public string? ParentTransactionCategoryName { get; }

    /// <summary>
    ///     If a parent category, its children.
    /// </summary>
    public DefaultTransactionCategory[]? ChildDefaultTransactionCategories { get; }
}