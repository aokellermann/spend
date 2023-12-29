using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Infrastructure;
using Spend.Graph.Infrastructure.DataLoaders;

namespace Spend.Graph.Domain.Entities.Transactions;

/// <summary>
///
/// </summary>
public class TransactionCategory : AuditableEntityWithTenantBase<ObjectId>
{
    /// <summary>
    ///     Short category name.
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    ///     Longer optional category description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    ///     The parent transaction category identifier if any. Only one level of nesting is allowed.
    /// </summary>
    public ObjectId? ParentTransactionCategoryId { get; init; }

    /// <summary>
    ///     The parent transaction category if any. Null if <see cref="ParentTransactionCategoryId"/> is null.
    /// </summary>
    public async Task<TransactionCategory?> GetParentTransactionCategory(TransactionCategoryDataLoader loader)
        => ParentTransactionCategoryId is null ? null : await loader.LoadAsync(ParentTransactionCategoryId!.Value);

    /// <summary>
    ///     The child transaction categories if any.
    /// </summary>
    public IExecutable<TransactionCategory> GetChildTransactionCategories(SpendDb db)
        => db.TransactionCategories.Find(x => x.ParentTransactionCategoryId == Id).AsExecutable();

    /// <summary>
    ///     Get transactions that have this proper category (transactions from child categories are not returned).
    /// </summary>
    public IExecutable<Transaction> GetTransactions(SpendDb db)
        => db.Transactions.Find(x => x.TransactionCategoryId == Id).AsExecutable();
}