using AestheticsLife.Training.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.Training.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrainingServices(this IServiceCollection services)
        => services
            .AddScoped<IExerciseService, ExerciseService>();
}