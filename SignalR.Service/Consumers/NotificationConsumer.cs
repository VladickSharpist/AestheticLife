using Aesthetic.SignalR.Services.Abstractions.Interfaces;
using MassTransit;
using RabbitMq;

namespace SignalR.Service.Consumers;

public class NotificationConsumer: IConsumer<NotificationEvent>
{
    private readonly INotificationService _notificationService;

    public NotificationConsumer(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<NotificationEvent> context)
    {
        await _notificationService.NotifyAsync(context.Message.UserId.ToString(), context.Message.Message);
    }
}