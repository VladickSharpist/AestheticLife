using AestheticsLife.DataAccess.Shared.Extensions;
using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthDataAccess(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDbContext<AuthDbContext>(opt => 
                opt.UseSqlServer(configuration.GetConnectionString("Default")))
            .AddUnitOfWork<AuthDbContext>()
            .AddIdentity();

    private static IServiceCollection AddIdentity(this IServiceCollection services)
        => services
            .AddIdentityCore<AuthUser>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .Services;
}