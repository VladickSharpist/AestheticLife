using AestheticLife.Web.Core.Controllers;
using AestheticsLife.Training.Services.Abstractions;
using AestheticsLife.Training.Services.Abstractions.Models;
using AestheticsLife.Web.Training.Models.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AestheticsLife.Web.Training.Controllers;

public class ExerciseController : BaseWebController
{
    private readonly IExerciseService _exerciseService;
    
    public ExerciseController(
        IMapper mapper,
        IExerciseService exerciseService) : base(mapper)
    {
        _exerciseService = exerciseService;
    }

    [HttpPost]
    public async Task<ActionResult<long>> AddExercise([FromBody] AddExerciseRequestVm model)
        => await _exerciseService.AddExerciseAsync(_mapper.Map<ExerciseDto>(model));

    [HttpPost]
    public async Task<ActionResult<string>> UploadExerciseVideo([FromBody] UploadExerciseVideoRequestVm model)
        => await _exerciseService.RequestVideoUpload(_mapper.Map<UploadFileRequestDto>(model));
}