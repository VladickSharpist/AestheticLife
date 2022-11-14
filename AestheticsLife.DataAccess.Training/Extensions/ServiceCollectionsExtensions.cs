using AestheticsLife.DataAccess.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.DataAccess.Training.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddTrainingDataAccess(this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<AestheticsLifeTrainingDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Default")))
            .AddUnitOfWork<AestheticsLifeTrainingDbContext>();
}