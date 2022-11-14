using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AestheticLife.DataAccess;

public class AestheticLifeDbContextFactory : IDesignTimeDbContextFactory<AestheticLifeDbContext>
{
    private const string CONNECTING_STRING = "Server=(LocalDb)\\MSSQLLocalDB;Database=AestheticLife;Trusted_Connection=True";

    public AestheticLifeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AestheticLifeDbContext>();
        optionsBuilder.UseSqlServer(CONNECTING_STRING);

        return new AestheticLifeDbContext(optionsBuilder.Options);
    }
}