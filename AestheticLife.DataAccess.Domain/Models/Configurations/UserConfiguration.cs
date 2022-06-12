using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticLife.DataAccess.Domain.Models.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.ActualRefreshToken)
            .IsRequired(false);

        builder
            .Property(u => u.Name)
            .IsRequired(false);
        
        builder
            .Property(u => u.Surname)
            .IsRequired(false);
        
        builder
            .Property(u => u.SecondName)
            .IsRequired(false);
    }
}