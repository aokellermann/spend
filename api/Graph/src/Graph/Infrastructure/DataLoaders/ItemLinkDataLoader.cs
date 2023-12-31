using MongoDB.Bson;
using Spend.Graph.Domain.Entities.Items;

namespace Spend.Graph.Infrastructure.DataLoaders;

public class ItemLinkDataLoader : EntityWithTenantDataLoaderBase<ObjectId, ItemLink>
{
    public ItemLinkDataLoader(UserContext ctx, SpendDb db, IBatchScheduler batchScheduler, DataLoaderOptions? options = null)
        : base(ctx, db, batchScheduler, options)
    {
    }
}