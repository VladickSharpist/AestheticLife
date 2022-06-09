using System.Security.Policy;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;
﻿using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Auth.Services.Implementations;

internal class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;

    public AuthService(
        IMapper mapper,
        UserManager<User> userManager,
        IEmailService emailService,
        ITokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
        _tokenService = tokenService;
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

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email.ToUpper());
        if (user is null) return "User not found";
        var response = await _userManager.CheckPasswordAsync(user, dto.Password);
        if(!response) return "Wrong password";
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        return refreshToken;
    }
}