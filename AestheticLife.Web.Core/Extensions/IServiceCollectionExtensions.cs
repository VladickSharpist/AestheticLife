using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.Web.Core.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
        => services;
}