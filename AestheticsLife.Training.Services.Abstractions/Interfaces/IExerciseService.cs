using AestheticsLife.Training.Services.Abstractions.Models;

namespace AestheticsLife.Training.Services.Abstractions;

public interface IExerciseService
{
    Task<long> AddExerciseAsync(ExerciseDto dto);

    Task<string> RequestVideoUpload(UploadFileRequestDto dto);
}