using System.Reflection;
using System.Text;
using AestheticLife.Auth.Services.Abstractions.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AestheticsLife.User.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
        => services
            .AddAutoMapper(Assembly.GetExecutingAssembly());
}