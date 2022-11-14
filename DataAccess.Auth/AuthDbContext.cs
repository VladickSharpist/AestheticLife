using DataAccess.Auth.Abstractions.Extensions;
using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Auth;

public class AuthDbContext: 
    IdentityDbContext<AuthUser, IdentityRole<long>, long, IdentityUserClaim<long>, 
        IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    public AuthDbContext(
        DbContextOptions<AuthDbContext> options): base(options)
    {
        string? envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (!string.IsNullOrEmpty(envName) && envName == "Development")
        {
            Database.EnsureCreated();
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SeedData();
    }
}