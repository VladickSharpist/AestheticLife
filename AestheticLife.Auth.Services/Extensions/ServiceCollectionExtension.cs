using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.Auth.Services.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        => services
            .AddScoped<IAuthService, AuthService>()
            //.AddScoped<IEmailService, EmailService>()
            .AddScoped<ITokenService, TokenService>();

    public static IServiceCollection AddUser(this IServiceCollection services)
        => services
            .AddScoped<CurrentUserAccessor>()
            .AddScoped<IUserSetter>(di => di.GetRequiredService<CurrentUserAccessor>())
            .AddScoped<IUserGetter>(di => di.GetRequiredService<CurrentUserAccessor>())
            .AddScoped<IUserService, UserService>();
}