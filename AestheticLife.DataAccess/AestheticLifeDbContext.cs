using AestheticLife.DataAccess.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

public class AestheticLifeDbContext : DbContext
{
    public AestheticLifeDbContext(DbContextOptions<AestheticLifeDbContext> options)
        :base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEntities();
    }
}