using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Infrastructure;

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
    public Task<ItemLink> GetItemLink(SpendDb db)
        => db.ItemLinks.Find(x => x.Id == ItemLinkId).FirstAsync();

    /// <summary>
    ///     The plaid transaction.
    /// </summary>
    public Going.Plaid.Entity.Transaction PlaidTransaction { get; init; } = default!;
}