using AestheticsLifeUI.DataAccess.Models.Login;
using AestheticsLifeUI.Services.Abstractions;
using Microsoft.AspNetCore.Components;

namespace AestheticsLifeUI.Pages.Auth;

public partial class Login
{
    [Inject] public IAuthService AuthService { get; set; }
    public LoginRequestVm RequestVm { get; set; } = new ();

    public void Submit()
    {
        AuthService.Authenticate(RequestVm);
    }
}