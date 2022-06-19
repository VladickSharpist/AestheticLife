using AestheticsLife.File.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.File.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileServices(this IServiceCollection services)
        => services
            .AddScoped<IFileService, FileService>();
}