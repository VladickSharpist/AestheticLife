using Logic.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTokenServices(this IServiceCollection services)
        => services
            .AddScoped<ITokenService, TokenService>();
    
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        => services
            .AddScoped<CurrentUser>()
            .AddScoped<ICurrentUserSetter>(di => di.GetService<CurrentUser>())
            .AddScoped<ICurrentUserGetter>(di => di.GetService<CurrentUser>());
    
}