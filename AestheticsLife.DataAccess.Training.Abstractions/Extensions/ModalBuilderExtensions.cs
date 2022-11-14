using AestheticsLife.DataAccess.Training.Abstractions.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AestheticsLife.DataAccess.Training.Abstractions.Extensions;

public static class ModalBuilderExtensions
{
    public static ModelBuilder ApplyTrainingConfigurations(this ModelBuilder builder)
        => builder.ApplyConfiguration(new ExerciseConfiguration());
}