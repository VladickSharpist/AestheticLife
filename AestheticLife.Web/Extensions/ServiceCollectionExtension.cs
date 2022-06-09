using System.Reflection;
using FluentValidation.AspNetCore;

namespace AestheticLife.Web.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static IServiceCollection AddValidator(this IServiceCollection services)
        => services.AddFluentValidation(cfg
            => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
}