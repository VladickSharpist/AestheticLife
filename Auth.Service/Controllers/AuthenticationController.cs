using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using Auth.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IAuthService authService,
        IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    [HttpPost("registration")]
    public async Task<ActionResult<TokenResponseVm>> Registration([FromBody] RegistrationVm model)
        => _mapper.Map<TokenResponseVm>(
            await _authService.RegisterAsync(_mapper.Map<RegisterUserDto>(model)));

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseVm>> Login([FromBody] LoginRequestVm model)
        => _mapper.Map<TokenResponseVm>(await _authService.LoginAsync(_mapper.Map<LoginDto>(model)));
    
    [HttpGet("refresh/{refreshToken}")]
    public async Task<ActionResult<RefreshResponseVm>> Refresh(string refreshToken)
        => _mapper.Map<RefreshResponseVm>(await _authService.RefreshAsync(refreshToken));
    
}