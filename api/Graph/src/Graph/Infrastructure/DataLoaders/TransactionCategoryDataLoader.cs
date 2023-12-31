using MongoDB.Bson;
using Spend.Graph.Domain.Entities.Transactions;

namespace Spend.Graph.Infrastructure.DataLoaders;

public class TransactionCategoryDataLoader : EntityWithTenantDataLoaderBase<ObjectId, TransactionCategory>
{
    public TransactionCategoryDataLoader(UserContext ctx, SpendDb db, IBatchScheduler batchScheduler, DataLoaderOptions? options = null)
        : base(ctx, db, batchScheduler, options)
    {
    }
}