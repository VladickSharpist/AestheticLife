using DataAccess.Auth.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Auth;

public class AuthDbContext: DbContext
{
    public AuthDbContext(
        DbContextOptions<AuthDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAuthConfigurations();
    }
}