using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using Auth.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IAuthService authService,
        ITokenService tokenService,
        IMapper mapper)
    {
        _authService = authService;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    [HttpPost("registration")]
    public async Task<ActionResult<bool>> Registration([FromBody] RegistrationVm model)
        => new (await _authService.RegisterAsync(
            _mapper.Map<RegisterUserDto>(model)));

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseVm>> Login([FromBody] LoginRequestVm model)
        => _mapper.Map<LoginResponseVm>(await _authService.LoginAsync(_mapper.Map<LoginDto>(model)));

    [Authorize]
    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshResponseVm>> Refresh(string refreshToken)
        => _mapper.Map<RefreshResponseVm>(await _tokenService.RefreshAsync(refreshToken));
    
}