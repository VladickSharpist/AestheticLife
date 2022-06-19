using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.Training.Services.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrainingServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}