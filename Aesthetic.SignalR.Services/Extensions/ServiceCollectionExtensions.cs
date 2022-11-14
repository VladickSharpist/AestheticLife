using Aesthetic.SignalR.Services.Abstractions.Interfaces;
using Aesthetic.SignalR.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Aesthetic.SignalR.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationService(this IServiceCollection services)
    {
        return services.AddScoped<INotificationService, NotificationService>();
    }
}