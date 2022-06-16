using System.Reflection;
using FluentValidation.AspNetCore;

namespace AestheticsLife.Web.Training.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrainingWebMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
    
    public static IServiceCollection AddValidator(this IServiceCollection services)
        => services.AddFluentValidation(cfg
            => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
}