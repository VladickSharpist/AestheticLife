using AestheticsLife.DataAccess.Training.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AestheticsLife.DataAccess.Training;

public class AestheticsLifeTrainingDbContext: DbContext
{
    public AestheticsLifeTrainingDbContext(
        DbContextOptions<AestheticsLifeTrainingDbContext> options): base(options)
    {
        //Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyTrainingConfigurations();
    }
}