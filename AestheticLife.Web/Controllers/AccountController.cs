using System.Security.Claims;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Web.Core.Controllers;
using AutoMapper;
using AestheticLife.Web.Models.Request;
using AestheticLife.Web.Models.Response;
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
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(
        IAuthService authService,
        IMapper mapper,
        IEmailService emailService,
        ITokenService tokenService,
        IUserService userService,
        IHttpContextAccessor httpContextAccessor)
        :base(mapper)
    {
        _authService = authService;
        _emailService = emailService;
        _tokenService = tokenService;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<bool>> Registration([FromBody] RegistrationRequestVm model)
        => new (await _authService.RegisterAsync(
            _mapper.Map<RegisterUserDto>(model)));

    [HttpPost]
    public async Task<IActionResult> SendEmailConfirmation([FromBody] ConfirmUserEmailRequestVm model)
    {
        await _emailService.SendEmailAsync(_mapper.Map<ConfirmUserEmailDto>(model));

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<bool>> ConfirmEmail(string token, string userId)
        => new(await _emailService.ConfirmEmail(userId, token));

    [HttpPost]
    public async Task<ActionResult<LoginResponseVm>> Login([FromBody] LoginRequestVm model)
        => _mapper.Map<LoginResponseVm>(await _authService.LoginAsync(_mapper.Map<LoginDto>(model)));

    [HttpPost]
    public async Task<ActionResult<RefreshResponseVm>> Refresh(string refreshToken)
        => _mapper.Map<RefreshResponseVm>(await _tokenService.RefreshAsync(refreshToken));

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<CurrentUserResponseVm>> CurrentUserData()
    {
       var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "Id").Value;

        return Ok(_mapper.Map<CurrentUserResponseVm>(await _userService.GetCurrentUserAsync(userId)));
    }
}