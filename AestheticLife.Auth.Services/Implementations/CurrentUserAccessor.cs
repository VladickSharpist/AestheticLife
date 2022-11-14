using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;

namespace AestheticLife.Auth.Services.Implementations;

public class CurrentUserAccessor
    :IUserGetter, 
        IUserSetter
{
    private UserDto _user;
    public UserDto User
    {
        get => _user;
        set => _user = value;
    }
}