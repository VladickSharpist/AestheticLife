using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticLife.DataAccess.Domain.Models.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.Property(m => m.Duration)
            .IsRequired(false);
        
        builder.Property(m => m.Hash)
            .IsRequired(false);
    }
}