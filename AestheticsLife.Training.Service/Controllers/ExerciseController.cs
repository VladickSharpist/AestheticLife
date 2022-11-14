using Aesthetic.CQRS.Abstractions;
using Aesthetic.CQRS.Abstractions.Models.ExerciseDto;
using Aesthetic.CQRS.Commands;
using Aesthetic.CQRS.Queries;
using AestheticsLife.Training.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AestheticsLife.Training.Service.Controllers;

[ApiController]
[Route("/api/exercise")]
public class ExerciseController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _executor;
    private readonly IQueryExecutor<ExerciseDto> _query;

    public ExerciseController(
        IMapper mapper,
        ICommandExecutor executor,
        IQueryExecutor<ExerciseDto> query)
    {
        _mapper = mapper;
        _executor = executor;
        _query = query;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseVm>>> Get()
    {
        return Ok(_mapper.Map<IEnumerable<ExerciseVm>>(await _query.ExecuteAsync<IEnumerable<ExerciseDto>>()));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseVm>> Get(long id)
    {
        return Ok(_mapper.Map<ExerciseVm>(await _query.ExecuteAsync<ExerciseDto>(new Query(id))));
    }
    
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<long>> Post([FromBody] AddExerciseVm model)
        => await _executor.ExecuteAsync<long, ExerciseDto>(
            new AddCommand<ExerciseDto>(_mapper.Map<ExerciseDto>(model)));
    
    //[Authorize]
    [HttpPut]
    public async Task<ActionResult<ExerciseVm>> Put([FromBody] ExerciseVm model)
        => _mapper.Map<ExerciseVm>(await _executor.ExecuteAsync<ExerciseDto, ExerciseDto>(
            new UpdateCommand<ExerciseDto>(_mapper.Map<ExerciseDto>(model))));
    
    //[Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(long id)
        => await _executor.ExecuteAsync<bool, long>(new DeleteCommand(id));
    
    [HttpPost("prepareFileUpload/{id}")]
    public async Task<ActionResult<string>> Post(long id, [FromBody] FileUploadPrepareVm model)
        => await _executor.ExecuteAsync<string, ExerciseDto>(
            new PrepareFileUploadingForEntityCommand<ExerciseDto>(
                new ExerciseDto{ Id = id, FileName = model.FileName }));

    public class FileUploadPrepareVm
    {
        public string FileName { get; set; }
    }
}