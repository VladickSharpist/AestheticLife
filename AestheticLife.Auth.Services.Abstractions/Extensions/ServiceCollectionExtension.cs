using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.Auth.Services.Abstractions.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}