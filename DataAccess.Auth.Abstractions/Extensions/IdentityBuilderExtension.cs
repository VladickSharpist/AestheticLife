using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Abstractions.Providers;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Abstractions.Extensions;

public static class IdentityBuilderExtension
{
    public static IdentityBuilder AddDefaultTokenProvider(this IdentityBuilder builder)
        => builder
            .AddTokenProvider("Default", typeof(EmailConfirmationTokenProvider<AuthUser>));
}