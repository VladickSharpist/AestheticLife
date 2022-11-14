using Microsoft.AspNetCore.Components.Authorization;

namespace AestheticsLifeUI.Extensions;

public static class AuthenticationStateProviderExtensions
{
    public static async ValueTask<long?> GetUserIdAsync(this AuthenticationStateProvider provider)
    {
        var idString = (await provider.GetAuthenticationStateAsync())
            .User.Claims
            .FirstOrDefault(c => c.Type == "Id")
            ?.Value;
        if (!string.IsNullOrEmpty(idString))
            return long.Parse(idString);
        return null;
    }
}