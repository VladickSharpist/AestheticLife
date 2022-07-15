using System.Text;
using AestheticLife.Auth.Services.Abstractions.Extesions;
using AestheticLife.Auth.Services.Extensions;
using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.DataAccess.Extensions;
using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Domain.Models;
using AestheticLife.DataAccess.Stores;
using AestheticsLife.Core.Extensions;
using AestheticsLife.File.Services.Abstractions.Extensions;
using AestheticsLife.File.Services.Extensions;
using AestheticsLife.Training.Services.Abstractions.Extensions;
using AestheticsLife.Training.Services.Extensions;
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
            .AddMappers();

    private static IServiceCollection AddMappers(this IServiceCollection services)
        => services
            .AddAuthServicesMapper()
            .AddFileServicesMapper()
            .AddTrainingServicesMapper();

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
            .AddContext(config)
            .AddCustomRepositories();
        
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddUser()
            .AddAuthenticationService()
            .AddFileServices()
            .AddTrainingServices();
    
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
        => services
            .AddIdentityCore<User>(options =>
            {
                options.Tokens.ProviderMap.Add(
                    "Default",
                    new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<User>)));
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<AestheticLifeDbContext>()
            .AddDefaultTokenProvider()
            .Services
            .ConfigureJWT(config);
    
    public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JwtConfig");
        var secretKey = jwtConfig["Secret"];
        return services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["ValidIssuer"],
                    ValidAudience = jwtConfig["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            })
            .Services;
    }
}