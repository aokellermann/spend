using MongoDB.Driver;
using Spend.Graph.Domain.Entities;
using Spend.Graph.Domain.Errors;

namespace Spend.Graph.Infrastructure;

public static class MongoQueryExtensions
{
    public static FilterDefinition<TEntity> AuditableFilter<TId, TEntity>(this IMongoCollection<TEntity> collection,
        TId id, long version, UserContext ctx)
        where TId : notnull
        where TEntity : IEntity<TId>, IHasTenant, IVersioned
        => collection.IdFilter(id) &
           collection.VersionFilter(version) &
           collection.TenantFilter(ctx);

    public static FilterDefinition<TEntity> IdFilter<TId, TEntity>(this IMongoCollection<TEntity> collection, TId id)
        where TId : notnull
        where TEntity : IEntity<TId>
        => Builders<TEntity>.Filter.Eq(x => x.Id, id);

    public static FilterDefinition<TEntity> TenantFilter<TEntity>(this IMongoCollection<TEntity> collection,
        UserContext ctx)
        where TEntity : IHasTenant
        => Builders<TEntity>.Filter.Eq(x => x.UserId, ctx.UserId!.Value);

    public static FilterDefinition<TEntity> VersionFilter<TEntity>(this IMongoCollection<TEntity> collection,
        long version)
        where TEntity : IVersioned
        => Builders<TEntity>.Filter.Eq(x => x.Version, version);


    public static UpdateDefinition<TEntity> Audit<TEntity>(this UpdateDefinition<TEntity> update)
        where TEntity : IAuditable, IVersioned
        => update.CurrentDate().Version();

    public static UpdateDefinition<TEntity> CurrentDate<TEntity>(this UpdateDefinition<TEntity> update)
        where TEntity : IAuditable
        => update.CurrentDate(x => x.UpdatedAt);

    public static UpdateDefinition<TEntity> Version<TEntity>(this UpdateDefinition<TEntity> update)
        where TEntity : IVersioned
        => update.Inc(x => x.Version, 1);

    public static async Task<TEntity> FindRequiredAuditable<TId, TEntity>(this IMongoCollection<TEntity> collection,
    TId id, long version, UserContext ctx)
            where TId : notnull
            where TEntity : IEntity<TId>, IHasTenant, IVersioned
    {
        var cursor = await collection.FindAsync(collection.AuditableFilter(id, version, ctx));
        var res = await cursor.FirstOrDefaultAsync();
        return res ?? throw new NotFoundException(typeof(TEntity), id);
    }
 }