using MongoDB.Bson;
using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.Domain.Entities.Transactions;

/// <summary>
///     Synchronization state for an item's transactions.
/// </summary>
public class TransactionsSyncState : AuditableEntityWithTenantBase<ObjectId>
{
    /// <summary>
    ///     The <see cref="ItemLink"/> identifier associated with the transaction sync state.
    /// </summary>
    public ObjectId ItemLinkId { get; init; }

    /// <summary>
    ///     The <see cref="ItemLink"/> associated with the transaction sync state.
    /// </summary>
    public Task<ItemLink> GetItemLink(SpendDb db)
        => db.ItemLinks.Find(x => x.Id == ItemLinkId).FirstAsync();

    /// <summary>
    ///     Cursor used for fetching any future updates after the latest update provided in this response. The cursor
    ///     obtained after all pages have been pulled (indicated by has_more being false) will be valid for at least
    ///     1 year. This cursor should be persisted for later calls. If transactions are not yet available, this will
    ///     be an empty string.
    /// </summary>
    public string Cursor { get; init; } = default!;
}