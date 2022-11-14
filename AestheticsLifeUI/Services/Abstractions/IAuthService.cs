using AestheticsLifeUI.DataAccess.Models.Login;

namespace AestheticsLifeUI.Services.Abstractions;

public interface IAuthService
{
    void Authenticate(LoginRequestVm model);
}