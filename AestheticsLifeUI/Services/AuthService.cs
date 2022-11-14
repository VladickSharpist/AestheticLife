using AestheticsLifeUI.DataAccess;
using AestheticsLifeUI.DataAccess.Abstractions;
using AestheticsLifeUI.DataAccess.Models.Login;
using AestheticsLifeUI.Services.Abstractions;
using Blazorise;

namespace AestheticsLifeUI.Services;

public class AuthService: IAuthService
{
    private readonly ILoginService _loginService;
    private readonly IHttpService _httpService;
    private readonly INotificationService _notificationService;

    public AuthService(ILoginService loginService, IHttpService httpService, INotificationService notificationService)
    {
        _loginService = loginService;
        _httpService = httpService;
        _notificationService = notificationService;
    }

    public async void Authenticate(LoginRequestVm model)
    {
        var response = await _httpService.PostAsync<LoginRequestVm, TokenResponseVm>(
            _httpService.BuildUrl("auth", "login"),
            model);
        if (response.Success)
        {
           await _loginService.Login(response.Response);
           await _notificationService
               .Success("Hello, Welcome back", options: opt => opt.IntervalBeforeClose = 2000);
        }
    }
}