using Aesthetic.CQRS.Abstractions;
using Aesthetic.CQRS.Abstractions.Models.UserDto;
using Aesthetic.CQRS.Commands;
using Aesthetic.CQRS.Queries;
using AestheticsLife.User.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AestheticsLife.User.Service.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly ICommandExecutor _executor;
    private readonly IQueryExecutor<UserDto> _query;
    private readonly IMapper _mapper;

    public UserController(IMapper mapper, ICommandExecutor executor, IQueryExecutor<UserDto> query)
    {
        _mapper = mapper;
        _executor = executor;
        _query = query;
    }

    [HttpGet]
    public async Task<IEnumerable<UserVm>> Get()
    {
        return _mapper.Map<IEnumerable<UserVm>>(await _query.ExecuteAsync<IEnumerable<UserDto>>());
    }
    
    [HttpGet("{id}")]
    public async Task<IEnumerable<UserVm>> Get(long id)
    {
        return _mapper.Map<IEnumerable<UserVm>>(await _query.ExecuteAsync<UserDto>(new Query(id)));
    }

    [HttpPut]
    public async Task<ActionResult<UserVm>> Put([FromBody] UserVm model)
        => _mapper.Map<UserVm>(await _executor.ExecuteAsync<UserDto, UserDto>(
            new UpdateCommand<UserDto>(_mapper.Map<UserDto>(model))));
    
    // [HttpDelete("{id}")]
    // public async Task<bool> Delete(long id)
    // {
    //     return await _executor.ExecuteAsync<bool, long>(new DeleteCommand(id));
    // }
}