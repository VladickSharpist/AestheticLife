using System.Security.Policy;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Auth.Services.Implementations;

internal class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public AuthService(
        IMapper mapper,
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }


    public async Task<bool> RegisterAsync(RegisterUserDto userDto)
    {
        // circus dont't look
        if (await _userManager.FindByEmailAsync(userDto.Email.ToUpper()) is not null)
            throw new Exception("User with this email already exists");

        return (await _userManager.CreateAsync(_mapper.Map<User>(userDto), userDto.Password)).Succeeded
            ? true
            : throw new Exception("Unknown error");
    }
}