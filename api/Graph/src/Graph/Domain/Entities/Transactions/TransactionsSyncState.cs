using System.ComponentModel.DataAnnotations;
using Graph.Domain.Entities.Items;
using MongoDB.Bson;

namespace Graph.Domain.Entities.Transactions;

public class TransactionsSyncState : IHasTenant, IAuditable, IVersioned
{
    public ObjectId Id { get; set; }

    public Guid UserId { get; init; }

    public ObjectId ItemLinkId { get; set; }

    public string Cursor { get; set; }

    public long Version { get; set; }

    public DateTime InsertedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}