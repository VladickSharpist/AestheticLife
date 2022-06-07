using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Web.Core.Controllers;
using AestheticLife.Web.Models2.Request;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : BaseWebController
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;

    public AccountController(
        IAuthService authService,
        IMapper mapper,
        IEmailService emailService)
        :base(mapper)
    {
        _authService = authService;
        _emailService = emailService;
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
}