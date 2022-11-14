using System.Reflection;

namespace AestheticsLife.Training.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
        => services
            .AddAutoMapper(Assembly.GetExecutingAssembly());
}