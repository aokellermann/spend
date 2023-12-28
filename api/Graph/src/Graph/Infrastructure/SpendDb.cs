using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Domain.Entities.Transactions;

namespace Spend.Graph.Infrastructure;

/// <summary>
///     Spend db context.
/// </summary>
public class SpendDb
{
    /// <summary>
    ///     Ctor.
    /// </summary>
    /// <param name="db">Mongo database.</param>
    public SpendDb(IMongoDatabase db)
    {
        Db = db;
        ItemLinks = db.GetCollection<ItemLink>("ItemLinks");
        TransactionSyncStates = db.GetCollection<TransactionsSyncState>("TransactionsSyncStates");
        Transactions = db.GetCollection<Transaction>("Transactions");
    }

    /// <summary>
    ///     The mongo database.
    /// </summary>
    public IMongoDatabase Db { get; }

    /// <summary>
    ///     Item links collection
    /// </summary>
    public IMongoCollection<ItemLink> ItemLinks { get; }

    /// <summary>
    ///     Transaction sync states collection.
    /// </summary>
    public IMongoCollection<TransactionsSyncState> TransactionSyncStates { get; }

    /// <summary>
    ///     Transactions collection.
    /// </summary>
    public IMongoCollection<Transaction> Transactions { get; }
}