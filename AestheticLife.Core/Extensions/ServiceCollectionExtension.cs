using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHelpers(
        this IServiceCollection services, 
        IConfiguration configuration)
        => services
            .AddScoped<IConfigurationHelper>(di => new ConfigurationHelper(configuration))
            .AddScoped<IUserTwoFactorTokenProvider<User>, EmailConfirmationTokenProvider<User>>();
}