using Microsoft.AspNetCore.SignalR;

namespace Aesthetic.SignalR.Services.Abstractions.Providers;

public class UserIdProvider: IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        var userId = connection.User.Claims.SingleOrDefault(c => c.Type == "Id")?.Value;
        return userId;
    }
}