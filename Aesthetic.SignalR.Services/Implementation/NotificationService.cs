using Aesthetic.SignalR.Services.Abstractions.Hubs;
using Aesthetic.SignalR.Services.Abstractions.Interfaces;
using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Aesthetic.SignalR.Services.Implementation;

public class NotificationService : INotificationService
{
    private NotificationHub _hubContext;
    private UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;


    public NotificationService(
        NotificationHub hubContext,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _hubContext = hubContext;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }


    public async Task NotifyAsync(string actorId, string message)
    {
        var user = await _userManager.FindByIdAsync(actorId);
        await _unitOfWork
            .GetReadWriteRepository<Notification>()
            .SaveAsync(new Notification
            {
                UserId = user.Id,
                Message = message
            });
        await _hubContext.
            Clients
            .User(actorId)
            .SendNotificationAsync(message);
    }
}