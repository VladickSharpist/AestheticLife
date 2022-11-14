using AestheticLife.Auth.Services.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface IUserService
{
    Task SetCurrentUserAsync(string userId);
}