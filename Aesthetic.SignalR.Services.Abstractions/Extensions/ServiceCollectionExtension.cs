using Aesthetic.SignalR.Services.Abstractions.Providers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Aesthetic.SignalR.Services.Abstractions.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSignalRUserIdProvider(this IServiceCollection services)
        => services
            .AddSingleton<IUserIdProvider, IdUserIdProvider>();
}