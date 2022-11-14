using AestheticLife.Core.Abstractions.Constants;
using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Auth.Abstractions.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<AuthUser>
{
    public void Configure(EntityTypeBuilder<AuthUser> builder)
    {
        builder
            .Property(u => u.ActualRefreshToken)
            .IsRequired(false);

        builder
            .HasData(new AuthUser
            {
                Id = 1,
                UserName = UserConstants.ADMIN_NAME,
                NormalizedUserName = UserConstants.ADMIN_NAME.ToUpper(),
                Email = UserConstants.ADMIN_EMIAL,
                NormalizedEmail = UserConstants.ADMIN_EMIAL.ToUpper(),
                PasswordHash = GetPasswordHash(UserConstants.ADMIN_PASSWORD),
                ConcurrencyStamp = "85263788-277f-4f89-b8c4-a11ac465ed58"
            });
    }


    private static string GetPasswordHash(string password)
    {
        var user = new AuthUser();
        var passwordHasher = new PasswordHasher<AuthUser>();
        var passwordHash = passwordHasher.HashPassword(user, password);

        return passwordHash;
    }
}