using Graph.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Graph.Infrastructure;

public class SpendDbContext : DbContext
{
    public SpendDbContext(DbContextOptions<SpendDbContext> options)
        : base(options)
    {

    }

    public DbSet<ItemLink> ItemLink => Set<ItemLink>();
}