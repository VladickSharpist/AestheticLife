using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Web.Core.Controllers;
using AestheticLife.Web.Models2.Request;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using AestheticLife.Web.Core;
using AestheticLife.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : BaseWebController
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;
    private ITokenService _tokenService;

    public AccountController(
        IAuthService authService,
        IMapper mapper,
        IEmailService emailService,
        ITokenService tokenService)
        :base(mapper)
    {
        _authService = authService;
        _emailService = emailService;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegistrationRequestVm model)
        => Ok(await _authService.RegisterAsync(
            _mapper.Map<RegisterUserDto>(model)));

    [HttpPost]
    public async Task<IActionResult> SendEmailConfirmation([FromBody] ConfirmUserEmailRequestVm model)
    {
        await _emailService.SendEmailAsync(_mapper.Map<ConfirmUserEmailDto>(model));

        return Ok();
    } 

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string userId)
        => Ok(await _emailService.ConfirmEmail(userId, token));

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginVm model)
    {
        return await _authService.LoginAsync(new() {Email = model.Email, Password = model.Password});
    }

    [HttpPost]
    public async Task<ActionResult<object>> Refresh(string refreshToken)
        => new OkObjectResult(await _tokenService.RefreshAsync(refreshToken));
}