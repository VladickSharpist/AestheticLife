using AestheticLife.Core.Abstractions.Constants;
using AestheticsLife.DataAccess.User.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticsLife.DataAccess.User.Abstractions.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(u => u.Name)
            .IsRequired(false);

        builder
            .Property(u => u.Surname)
            .IsRequired(false);

        builder
            .Property(u => u.MiddleName)
            .IsRequired(false);

        builder
            .HasData(new ApplicationUser
            {
                Id = 1,
                Name = UserConstants.ADMIN_NAME,
                Surname = UserConstants.ADMIN_NAME,
                Email = UserConstants.ADMIN_EMIAL
            });
    }
}