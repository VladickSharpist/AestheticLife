using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface IUserGetter
{
    UserDto User { get; }
}