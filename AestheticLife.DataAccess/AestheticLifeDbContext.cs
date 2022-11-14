using AestheticLife.DataAccess.Domain.Extensions;
using AestheticLife.DataAccess.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

public class AestheticLifeDbContext : DbContext
{
    public AestheticLifeDbContext(DbContextOptions<AestheticLifeDbContext> options)
        :base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEntities();
    }
}