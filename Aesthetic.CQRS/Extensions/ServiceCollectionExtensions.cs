using System.Reflection;
using Aesthetic.CQRS.Abstractions;
using Aesthetic.CQRS.Abstractions.Extensions;
using Aesthetic.CQRS.Abstractions.Models.ExerciseDto;
using Aesthetic.CQRS.Abstractions.Models.UserDto;
using Aesthetic.CQRS.Handlers;
using AestheticLife.Auth.Services.Extensions;
using AestheticsLife.DataAccess.Training.Abstractions.Models;
using AestheticsLife.DataAccess.User.Abstractions.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aesthetic.CQRS.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
        => services
            .AddCqrsMapper()
            .AddMediatrCqrs()
            .AddCommandExecutor()
            .ApplyCommands()
            .ApplyQueries();

    private static IServiceCollection AddMediatrCqrs(this IServiceCollection services)
        => services.AddMediatR(Assembly.GetExecutingAssembly());

    private static IServiceCollection AddCommandExecutor(this IServiceCollection services)
        => services
            .AddScoped<ICommandExecutor, CommandExecutor>();

    private static IServiceCollection ApplyCommands(this IServiceCollection services)
        => services
            .ApplyExerciseCommands()
            .ApplyUserCommands();
            
    
    private static IServiceCollection ApplyExerciseCommands(this IServiceCollection services)
        => services
            .AddScoped<IHandler<ICommand<ExerciseDto>, ExerciseDto, long>, 
                AddCommandHandler<ICommand<ExerciseDto>, ExerciseDto, Exercise>>()
            .AddScoped<IHandler<ICommand<ExerciseDto>, ExerciseDto, ExerciseDto>, 
                UpdateCommandHandler<ICommand<ExerciseDto>, ExerciseDto, Exercise>>()
            .AddScoped<IHandler<ICommand<long>, long, bool>, 
                DeleteCommandHandler<ICommand<long>, Exercise>>();
    
    private static IServiceCollection ApplyUserCommands(this IServiceCollection services)
        => services
            .AddScoped<IHandler<ICommand<UserDto>, UserDto, long>, 
                AddCommandHandler<ICommand<UserDto>, UserDto, ApplicationUser>>()
            .AddScoped<IHandler<ICommand<UserDto>, UserDto, UserDto>, 
                UpdateCommandHandler<ICommand<UserDto>, UserDto, ApplicationUser>>()
            .AddScoped<IHandler<ICommand<long>, long, bool>, 
                DeleteCommandHandler<ICommand<long>, ApplicationUser>>();

    private static IServiceCollection ApplyQueries(this IServiceCollection services)
        => services
            .AddScoped<IQueryExecutor<ExerciseDto>, QueryExecutor<ExerciseDto, Exercise>>()
            .AddScoped<IQueryExecutor<UserDto>, QueryExecutor<UserDto, ApplicationUser>>();
}