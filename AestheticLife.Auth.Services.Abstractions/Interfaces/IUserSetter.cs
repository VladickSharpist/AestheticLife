using AestheticLife.Auth.Services.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface IUserSetter
{
    UserDto User { set; }
}