using AestheticsLife.DataAccess.User.Abstractions.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AestheticsLife.DataAccess.User.Abstractions.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyUserConfigurations(this ModelBuilder builder)
        => builder
            .ApplyConfiguration(new UserConfiguration());
}