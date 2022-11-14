using AestheticsLife.DataAccess.User.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AestheticsLife.DataAccess.User;

public class AestheticLifeUserDbContext: DbContext
{
    public AestheticLifeUserDbContext(
        DbContextOptions<AestheticLifeUserDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyUserConfigurations();
    }
}