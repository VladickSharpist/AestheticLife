using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.DataAccess.Abstractions;
using AestheticsLife.File.Services.Abstractions;
using AestheticsLife.File.Services.Abstractions.Models;
using AestheticsLife.Training.Services.Abstractions;
using AestheticsLife.Training.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticsLife.Training.Services;

public class ExerciseService : IExerciseService
{
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExerciseService(IFileService fileService, IUnitOfWork unitOfWork)
    {
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> AddExerciseAsync(ExerciseDto dto)
        => await _unitOfWork.GetReadWriteRepository<Exercise>().SaveAsync(_mapper.Map<Exercise>(dto));

    public async Task<string> RequestVideoUpload(UploadFileRequestDto dto)
        => await _fileService.CreateFileEntity<Exercise>(_mapper.Map<FileEntryDto>(dto));
}