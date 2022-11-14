using Logic.Shared.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Microservices.Shared.Middlewares;

public class CurrentUserSetterMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserSetterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        ICurrentUserSetter userSetter)
    {
        if (httpContext.User.Claims is not null && httpContext.User.Claims.ToList().Count > 0)
        {
            var userId = httpContext.User.Claims.SingleOrDefault(c => c.Type == "Id").Value;
            userSetter.CurrentUserId = long.Parse(userId);
        }

        await _next(httpContext);
    }
}