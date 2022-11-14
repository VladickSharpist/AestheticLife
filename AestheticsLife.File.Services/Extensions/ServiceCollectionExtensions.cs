using AestheticsLife.File.Services.Abstractions.Extensions;
using AestheticsLife.File.Services.Abstractions.Interfaces;
using AestheticsLife.File.Services.Implementations;
using Logic.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AestheticsLife.File.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileService(this IServiceCollection services)
        => services
            .AddFileServicesMapper()
            .AddTokenServices()
            .AddScoped<IFileService, FileService>();
}