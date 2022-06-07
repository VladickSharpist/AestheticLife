using System.Reflection;
using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.Core.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Web.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static IServiceCollection AddValidator(this IServiceCollection services)
        => services.AddFluentValidation(cfg
            => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

    public static IServiceCollection AddIdentity(this IServiceCollection services)
        => services.AddIdentityCore<User>(options =>
            {
                options.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<User>)));
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AestheticLifeDbContext>()
            .AddDefaultTokenProvider()
            .Services;
}