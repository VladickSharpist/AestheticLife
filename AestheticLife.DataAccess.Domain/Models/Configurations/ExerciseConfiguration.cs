using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AestheticLife.DataAccess.Domain.Models.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasOne<File>().WithOne().HasForeignKey<Exercise>(m => m.FileId);
        builder.HasOne<User>().WithMany(u => u.Exercises).HasForeignKey(e => e.OwnerId);
    }
}