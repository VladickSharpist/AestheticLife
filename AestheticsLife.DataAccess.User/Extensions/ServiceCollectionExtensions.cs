using AestheticsLife.DataAccess.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.DataAccess.User.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserDataAccess(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDbContext<AestheticLifeUserDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Default")))
            .AddUnitOfWork<AestheticLifeUserDbContext>();
}