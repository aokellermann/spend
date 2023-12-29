using MongoDB.Driver;
using Spend.Graph.Domain.Entities;

namespace Spend.Graph.Infrastructure.DataLoaders;

public class EntityWithTenantDataLoaderBase<TId, TEntity> : EntityDataLoaderBase<TId, TEntity>
    where TId : notnull
    where TEntity : IEntity<TId>, IHasTenant
{
    private readonly UserContext _ctx;

    public EntityWithTenantDataLoaderBase(UserContext ctx, SpendDb db, IBatchScheduler batchScheduler, DataLoaderOptions? options = null)
        : base(db, batchScheduler, options)
    {
        _ctx = ctx;
    }

    protected override async Task<FilterDefinition<TEntity>> GetFilterDefinition(IReadOnlyList<TId> keys, CancellationToken cancellationToken)
    {
        var filter = await base.GetFilterDefinition(keys, cancellationToken);
        return filter & Builders<TEntity>.Filter.Eq(x => x.UserId, _ctx.UserId!.Value);
    }
}