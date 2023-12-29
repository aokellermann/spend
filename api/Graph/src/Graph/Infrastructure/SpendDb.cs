using MongoDB.Driver;
using Spend.Graph.Domain.Entities.Items;
using Spend.Graph.Domain.Entities.Transactions;

namespace Spend.Graph.Infrastructure;

/// <summary>
///     Spend db context.
/// </summary>
public class SpendDb
{
    private static readonly IReadOnlyDictionary<Type, string> CollectionNames = new Dictionary<Type, string>()
    {
        [typeof(ItemLink)] = "ItemLinks",
        [typeof(TransactionsSyncState)] = "TransactionSyncStates",
        [typeof(Transaction)] = "Transactions",
        [typeof(TransactionCategory)] = "TransactionCategories",
    };

    /// <summary>
    ///     Ctor.
    /// </summary>
    /// <param name="db">Mongo database.</param>
    public SpendDb(IMongoDatabase db)
    {
        Db = db;
        ItemLinks = Collection<ItemLink>();
        TransactionSyncStates = Collection<TransactionsSyncState>();
        Transactions = Collection<Transaction>();
        TransactionCategories = Collection<TransactionCategory>();
    }

    /// <summary>
    ///     The mongo database.
    /// </summary>
    public IMongoDatabase Db { get; }

    public IMongoCollection<TEntity> Collection<TEntity>()
        => Db.GetCollection<TEntity>(CollectionNames[typeof(TEntity)]);

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

    /// <summary>
    ///     Transaction categories collection.
    /// </summary>
    public IMongoCollection<TransactionCategory> TransactionCategories { get; }
}