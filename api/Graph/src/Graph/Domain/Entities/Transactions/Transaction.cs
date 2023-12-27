using MongoDB.Bson;

namespace Graph.Domain.Entities.Transactions;

public class Transaction : IHasTenant, IAuditable, IVersioned
{
    public ObjectId Id { get; set; }
    public string ItemId { get; set; }
    public string PlaidTransactionId { get; set; }
    public Going.Plaid.Entity.Transaction PlaidTransaction { get; set; }

    public Guid UserId { get; set; }
    public DateTime InsertedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long Version { get; set; }
}