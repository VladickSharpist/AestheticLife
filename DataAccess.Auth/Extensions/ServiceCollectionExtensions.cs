using AestheticsLife.DataAccess.Shared.Extensions;
using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Abstractions.Repositories;
using DataAccess.Auth.Repositories;
using DataAccess.Auth.Stores;
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
            .AddCustomRepositories()
            .AddIdentity();
    
    private static IServiceCollection AddCustomRepositories(this IServiceCollection services)
        => services
            .AddCustomRepository<UserRole, IUserRoleRepository, UserRoleRepository>();

    private static IServiceCollection AddIdentity(this IServiceCollection services)
        => services
            .AddIdentityCore<AuthUser>()
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .Services;
}