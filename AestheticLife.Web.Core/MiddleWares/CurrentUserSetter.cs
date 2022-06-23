using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Core.Abstractions.Constants;
using Microsoft.AspNetCore.Http;

namespace AestheticLife.Web.Core.MiddleWares;

public class CurrentUserSetter
{
    private readonly RequestDelegate _next;


    public CurrentUserSetter(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(
        HttpContext httpContext,
        IUserService userService)
    {
        if (httpContext.User.Claims is not null && httpContext.User.Claims.ToList().Count > 0)
        {
            var userId = httpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimConstants.TYPE_ID).Value;
            await userService.SetCurrentUserAsync(userId);
        }

        await _next(httpContext);
    }
}