using Graph.Domain.Entities.Items;
using Graph.Domain.Entities.Transactions;
using MongoDB.Driver;

namespace Graph.Infrastructure;

public class SpendDb
{
    public SpendDb(IMongoDatabase db)
    {
        Db = db;
        ItemLinks = db.GetCollection<ItemLink>("ItemLinks");
        TransactionSyncStates = db.GetCollection<TransactionsSyncState>("TransactionsSyncStates");
        Transactions = db.GetCollection<Transaction>("Transactions");
    }

    public IMongoDatabase Db { get; }

    public IMongoCollection<ItemLink> ItemLinks { get; }
    public IMongoCollection<TransactionsSyncState> TransactionSyncStates { get; }
    public IMongoCollection<Transaction> Transactions { get; }
}