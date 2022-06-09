using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.Core.Helpers;
using Microsoft.AspNetCore.Identity;

namespace AestheticsLife.Core.Extensions;

public static class IdentityBuilderExtension
{
    public static IdentityBuilder AddDefaultTokenProvider(this IdentityBuilder builder)
        => builder
            .AddTokenProvider("Default", typeof(EmailConfirmationTokenProvider<User>));
}