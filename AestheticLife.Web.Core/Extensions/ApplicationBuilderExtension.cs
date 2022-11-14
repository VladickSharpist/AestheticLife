using Aesthetic.SignalR.Services.Abstractions.Hubs;
using AestheticLife.Web.Core.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AestheticLife.Web.Core.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ApplyCustomMiddlewares(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.CurrentUserSetter();

    public static IApplicationBuilder CurrentUserSetter(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<CurrentUserSetter>();
    
    public static WebApplication ApplyMiddlewares(this WebApplication applicationBuilder, WebApplicationBuilder builder)
    {
        if (applicationBuilder.Environment.IsDevelopment())
        {
            applicationBuilder.UseDeveloperExceptionPage();
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI();
        }

        applicationBuilder.UseHttpsRedirection();

        applicationBuilder.UseRouting();
        applicationBuilder.UseCors(builder.Services.GetUsingCors());
        applicationBuilder.UseAuthentication();
        applicationBuilder.UseAuthorization();
        applicationBuilder.CurrentUserSetter();
        applicationBuilder.MapControllers();

        return applicationBuilder;
    }
}