namespace Aesthetic.SignalR.Services.Abstractions.Interfaces;

public interface IClient
{
    Task SendNotificationAsync(string message);

    Task GetNewNotificationsAsync(IEnumerable<string> notifications);
}