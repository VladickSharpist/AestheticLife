using AestheticLife.Core.Abstractions.Constants;
using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Auth.Abstractions.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder SeedData(this ModelBuilder builder)
    {
        builder.Entity<AuthUser>()
            .HasData(new AuthUser
            {
                Id = -1,
                UserName = UserConstants.ADMIN_NAME,
                NormalizedUserName = UserConstants.ADMIN_NAME.ToUpper(),
                Email = UserConstants.ADMIN_EMIAL,
                NormalizedEmail = UserConstants.ADMIN_EMIAL.ToUpper(),
                PasswordHash = GetPasswordHash(UserConstants.ADMIN_PASSWORD),
                ConcurrencyStamp = "85263788-277f-4f89-b8c4-a11ac465ed58",
                SecurityStamp = Guid.NewGuid().ToString()
            });
        
        builder.Entity<IdentityRole<long>>()
            .HasData(new IdentityRole<long>
            {
                Id = -1,
                Name = RoleConstants.ROLE_ADMIN,
                NormalizedName = RoleConstants.ROLE_ADMIN.ToUpper(),
                ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41302"
            });

        builder.Entity<IdentityRole<long>>()
            .HasData(new IdentityRole<long>
            {
                Id = 1,
                Name = RoleConstants.ROLE_USER,
                NormalizedName = RoleConstants.ROLE_USER.ToUpper(),
                ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41301"
            });
        
        builder.Entity<IdentityUserRole<long>>()
            .HasData(new IdentityUserRole<long>
            {
                UserId = -1,
                RoleId = -1
            });
        
        return builder;
    }
    
    private static string GetPasswordHash(string password)
    {
        var user = new AuthUser();
        var passwordHasher = new PasswordHasher<AuthUser>();
        var passwordHash = passwordHasher.HashPassword(user, password);

        return passwordHash;
    }
}