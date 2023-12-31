using MongoDB.Driver;
using Spend.Graph.Domain.Entities;

namespace Spend.Graph.Infrastructure.DataLoaders;

public abstract class EntityDataLoaderBase<TId, TEntity> : BatchDataLoader<TId, TEntity>
    where TId : notnull
    where TEntity : IEntity<TId>
{
    private readonly SpendDb _db;

    protected EntityDataLoaderBase(SpendDb db, IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) :
        base(batchScheduler, options)
    {
        _db = db;
    }

    protected virtual Task<FilterDefinition<TEntity>> GetFilterDefinition(IReadOnlyList<TId> keys,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(Builders<TEntity>.Filter.In(x => x.Id, keys));
    }

    protected override async Task<IReadOnlyDictionary<TId, TEntity>> LoadBatchAsync(IReadOnlyList<TId> keys,
        CancellationToken cancellationToken)
    {
        var cursor = await _db.Collection<TEntity>().FindAsync(
            await GetFilterDefinition(keys, cancellationToken),
            new FindOptions<TEntity, TEntity>(), cancellationToken);

        var res = new Dictionary<TId, TEntity>();
        await cursor.ForEachAsync(x => res.Add(x.Id, x), cancellationToken);

        return res;
    }
}