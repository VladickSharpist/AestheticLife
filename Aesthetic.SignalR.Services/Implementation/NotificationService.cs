using Aesthetic.SignalR.Services.Abstractions.Hubs;
using Aesthetic.SignalR.Services.Abstractions.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Aesthetic.SignalR.Services.Implementation;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub, IClient> _hubContext;
    public NotificationService(IHubContext<NotificationHub, IClient> hubContext)
    {
        _hubContext = hubContext;
    }


    public async Task NotifyAsync(string actorId, string message)
    {
        await _hubContext.
            Clients
            .User(actorId)
            .SendNotificationAsync(message);
    }
}