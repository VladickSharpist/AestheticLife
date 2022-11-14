namespace Aesthetic.SignalR.Services.Abstractions.Interfaces;

public interface INotificationService
{
    Task NotifyAsync(string actorId, string message);
}