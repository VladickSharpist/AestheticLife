using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<AestheticLifeDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("AestheticLife")));

}