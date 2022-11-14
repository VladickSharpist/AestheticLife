using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.File.Services.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}