using AestheticsLifeUI.DataAccess.Models.Login;

namespace AestheticsLifeUI.Services.Abstractions;

public interface ILoginService
{
    Task Login(TokenResponseVm token);
    Task Logout();
}