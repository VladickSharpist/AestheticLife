using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.Auth.Services.Abstractions.Extesions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}