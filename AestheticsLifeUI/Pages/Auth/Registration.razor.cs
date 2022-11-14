using AestheticsLifeUI.DataAccess.Abstractions;
using AestheticsLifeUI.DataAccess.Models.Login;
using AestheticsLifeUI.DataAccess.Models.Registration;
using AestheticsLifeUI.Services.Abstractions;
using Microsoft.AspNetCore.Components;

namespace AestheticsLifeUI.Pages.Auth;

public partial class Registration
{
    [Inject] public IHttpService HttpService { get; set; }
    [Inject] public  ILoginService _loginService { get; set; }

    public RegistrationVm Model { get; set; } = new ();

    public async void Submit()
    {
        var response = await HttpService.PostAsync<RegistrationVm, TokenResponseVm>(
            HttpService.BuildUrl("Auth", "Registration"),
            Model);
        if (!response.Success)
        {
            await _loginService.Login(response.Response);
        }
    }
}