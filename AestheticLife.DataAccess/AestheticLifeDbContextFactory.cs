using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AestheticLife.DataAccess;

public class AestheticLifeDbContextFactory : IDesignTimeDbContextFactory<AestheticLifeDbContext>
{
    public AestheticLifeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AestheticLifeDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\Server;Database=Database;Trusted_Connection=True");

        return new AestheticLifeDbContext(optionsBuilder.Options);
    }
}