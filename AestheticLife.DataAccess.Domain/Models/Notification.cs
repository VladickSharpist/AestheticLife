using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;

namespace AestheticLife.DataAccess.Domain.Models;

public class Notification : IEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Message { get; set; }

    public User User { get; set; }
}