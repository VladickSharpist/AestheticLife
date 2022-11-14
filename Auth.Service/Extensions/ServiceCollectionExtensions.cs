using System.Reflection;
using AestheticLife.Auth.Services.Abstractions.Extensions;

namespace Auth.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
        => services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddAuthServicesMapper();
}

// options =>
// {
//     options.Tokens.ProviderMap.Add(
//         "Default",
//         new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<ApplicationUser>)));
// }.AddDefaultTokenProvider()