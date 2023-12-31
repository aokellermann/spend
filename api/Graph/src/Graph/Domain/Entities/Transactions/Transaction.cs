using MongoDB.Bson;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Infrastructure.DataLoaders;

namespace Spend.Graph.Domain.Entities.Transactions;

/// <summary>
///     A financial transaction.
/// </summary>
public class Transaction : AuditableEntityWithTenantBase<ObjectId>
{
    /// <summary>
    ///     The <see cref="ItemLink"/> identifier associated with the transaction.
    /// </summary>
    public ObjectId ItemLinkId { get; init; }

    /// <summary>
    ///     The <see cref="ItemLink"/> associated with the transaction.
    /// </summary>
    public Task<ItemLink> GetItemLink(ItemLinkDataLoader loader) => loader.LoadAsync(ItemLinkId);

    /// <summary>
    ///     The plaid transaction.
    /// </summary>
    public Going.Plaid.Entity.Transaction PlaidTransaction { get; init; } = default!;

    /// <summary>
    ///     The transaction category identifier associated with the transaction.
    /// </summary>
    public ObjectId TransactionCategoryId { get; init; }

    /// <summary>
    ///     The transaction category associated with the transaction.
    /// </summary>
    public Task<TransactionCategory> GetTransactionCategory(TransactionCategoryDataLoader loader)
        => loader.LoadAsync(TransactionCategoryId);
}