using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.Auth.Services.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        => services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IUserService, UserService>();
}