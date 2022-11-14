using AestheticLife.Core.Abstractions.Constants;
using Microsoft.AspNetCore.SignalR;

namespace Aesthetic.SignalR.Services.Abstractions.Providers;

public class IdUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Claims.SingleOrDefault(c => c.Type == ClaimConstants.TYPE_ID)?.Value;
    }
}