using Aesthetic.SignalR.Services.Abstractions.Extensions;
using AestheticLife.Auth.Services.Abstractions.Extensions;
using AestheticLife.Auth.Services.Extensions;
using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.DataAccess.Extensions;
using AestheticsLife.Core.Extensions;
using AestheticsLife.File.Services.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticLife.Web.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddHelpers(configuration)
            .AddMappers();

    private static IServiceCollection AddMappers(this IServiceCollection services)
        => services
            .AddAuthServicesMapper()
            .AddFileServicesMapper();

    public static IServiceCollection ApplyCors(
        this IServiceCollection services)
    {
        var cHelper = services
            .BuildServiceProvider()
            .GetRequiredService<IConfigurationHelper>();
        
        return services
            .AddCors(opt => opt
                .AddPolicy(cHelper.Policy, policy => 
                    policy
                        .WithOrigins(cHelper.AllowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));
    }

    public static string GetUsingCors(this IServiceCollection services)
        => services
            .BuildServiceProvider()
            .GetRequiredService<IConfigurationHelper>()
            .Policy;

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config);

    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddUser()
            .AddAuthenticationServices();
    
    public static IServiceCollection AddWebSockets(this IServiceCollection services)
        => services
            .AddSignalRUserIdProvider()
            .AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
            })
            .Services;
}