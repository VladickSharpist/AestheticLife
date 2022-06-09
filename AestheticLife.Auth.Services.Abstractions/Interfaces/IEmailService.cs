using AestheticLife.Auth.Services.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(ConfirmUserEmailDto modelDto);

    Task<bool> ConfirmEmail(string userId, string token);
}