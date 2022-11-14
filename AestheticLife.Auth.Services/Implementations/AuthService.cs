using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Core.Abstractions.Constants;
using AutoMapper;
using DataAccess.Auth.Abstractions.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using RabbitMq.Events;

namespace AestheticLife.Auth.Services.Implementations;

internal class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AuthUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthService(
        IMapper mapper,
        UserManager<AuthUser> userManager,
        ITokenService tokenService, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
        _publishEndpoint = publishEndpoint;
    }


    public async Task<bool> RegisterAsync(RegisterUserDto userDto)
    {
        if (await _userManager.FindByEmailAsync(userDto.Email.ToUpper()) is not null)
            throw new Exception("User with this email already exists");

        var user = _mapper.Map<AuthUser>(userDto);
        var registerResult = await _userManager.CreateAsync(user, userDto.Password);
        if (!registerResult.Succeeded)
            throw new Exception(String.Join(";;;", registerResult.Errors.Select(error => error.Description)));

        var addingToRoleResult = await _userManager.AddToRoleAsync(user, RoleConstants.ROLE_USER);
        if (!addingToRoleResult.Succeeded)
            throw new Exception($"An adding the user \"{user.UserName}\" with the email \"{user.Email}\" " +
                                $"to the role \"{RoleConstants.ROLE_USER}\" is failed.");
        
        await _publishEndpoint.Publish(new NewUserRegistered { Email = user.Email });
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