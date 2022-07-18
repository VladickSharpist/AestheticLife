using Aesthetic.SignalR.Services.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Aesthetic.SignalR.Services.Abstractions.Hubs;

public class NotificationHub : Hub<IClient>
{
    public async Task SendNotification(string user, string message)
        => await Clients.All.SendNotificationAsync("Hello, gym boy");
}