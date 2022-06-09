using AestheticLife.Auth.Services.Extensions;
using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.DataAccess.Extensions;
﻿using System.Text;
using AestheticLife.Auth.Services.Extensions;
using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Domain.Models;
using AestheticLife.DataAccess.Extensions;
using AestheticLife.DataAccess.Stores;
using AestheticsLife.Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AestheticLife.Web.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddHelpers(configuration)
            .AddAuthenticationService();

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
                        .AllowAnyMethod()));
    }
    
    public static string GetUsingCors(this IServiceCollection services)
        => services
            .BuildServiceProvider()
            .GetRequiredService<IConfigurationHelper>()
            .Policy;

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        => services.AddContext(config);
        
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddAuthServices();
    
    public static IServiceCollection AddIdentity(this IServiceCollection services)
        => services
            .AddIdentity<User, Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<AestheticLifeDbContext>()
            .Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters();
            })
            .Services;
}