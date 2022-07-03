using System.Security.Policy;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;
﻿using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Core.Abstractions.Constants;
using AestheticLife.DataAccess.Domain.Models;
using AestheticLife.DataAccess.Domain.Models.Configurations;
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
        if (await _userManager.FindByEmailAsync(userDto.Email.ToUpper()) is not null)
            throw new Exception("User with this email already exists");

        var user = _mapper.Map<User>(userDto);
        var registerResult = await _userManager.CreateAsync(user, userDto.Password);
        if (!registerResult.Succeeded)
            throw new Exception("Unknown error");

        var addingToRoleResult = await _userManager.AddToRoleAsync(user, RoleConstants.ROLE_USER);
        if (!addingToRoleResult.Succeeded)
            throw new Exception($"An adding the user \"{user.UserName}\" with the email \"{user.Email}\" " +
                                $"to the role \"{RoleConstants.ROLE_USER}\" is failed.");

        return true;
    }

    public async Task<TokenDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email.ToUpper());
        if (user is null)
            throw new Exception("User not found");
        var response = await _userManager.CheckPasswordAsync(user, dto.Password);
        if(!response)
            throw new Exception("Wrong Password");

        var refreshTokenRecord = await _tokenService.GenerateRefreshTokenAsync(user);
        var accessTokenRecord = await _tokenService.SetAccessTokenAsync(user);
        return new()
        {
            RefreshToken = refreshTokenRecord.refreshToken,
            AccessToken = accessTokenRecord.accessToken,
            RefreshTokenExpiresAt = refreshTokenRecord.expiresAt,
            AccessTokenExpiresAt = accessTokenRecord.expiresAt
        };
    }
}