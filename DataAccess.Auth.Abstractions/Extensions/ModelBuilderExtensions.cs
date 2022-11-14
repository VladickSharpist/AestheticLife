using DataAccess.Auth.Abstractions.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Auth.Abstractions.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyAuthConfigurations(this ModelBuilder builder)
        => builder
            .ApplyConfiguration(new RoleConfiguration())
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new UserRoleConfiguration());
}