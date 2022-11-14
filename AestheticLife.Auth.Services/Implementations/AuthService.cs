using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Core.Abstractions.Constants;
using AutoMapper;
using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Extensions;
using Logic.Shared.Abstractions;
using Logic.Shared.Abstractions.Models;
using Logic.Shared.Abstractions.Records;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RabbitMq.Events;

namespace AestheticLife.Auth.Services.Implementations;

internal class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AuthUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IMapper mapper,
        UserManager<AuthUser> userManager,
        ITokenService tokenService, IPublishEndpoint publishEndpoint, ILogger<AuthService> logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }


    public async Task<TokenDto> RegisterAsync(RegisterUserDto userDto)
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
        return await LoginAsync(new LoginDto
        {
            Email = userDto.Email,
            Password = userDto.Password
        });
    }

    public async Task<TokenDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email.ToUpper());
        if (user is null)
        {
            _logger.Log(LogLevel.Warning, "User not found");
            throw new Exception("User not found");
        }

        var response = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!response)
            throw new Exception("Wrong Password");

        var refreshTokenRecord = await _tokenService.GenerateRefreshTokenAsync(user.Id);
        var encodedToken = await _userManager.SetActiveRefreshTokenAsync(user, refreshTokenRecord.refreshToken);
        var accessTokenRecord = await SetAccessTokenAsync(user);
        return new()
        {
            RefreshToken = refreshTokenRecord.refreshToken,
            AccessToken = accessTokenRecord.accessToken,
            RefreshTokenExpiresAt = refreshTokenRecord.expiresAt,
            AccessTokenExpiresAt = accessTokenRecord.expiresAt
        };
    }

    public async Task<TokenDto> RefreshAsync(string refreshToken)
    {
        var decodedToken = _tokenService.DecodeToken<RefreshTokenDto>(refreshToken);
        var user = await _userManager.FindByIdAsync(decodedToken.UserId.ToString());
        var decodedActualUserRefreshToken = _tokenService.DecodeToken<RefreshTokenDto>(user.ActualRefreshToken);
        if (decodedToken.IsExpired
            || user is null
            || decodedActualUserRefreshToken.IsExpired
            || refreshToken != user.ActualRefreshToken)
            throw new Exception("Invalid refresh token");
        var refreshTokenRecord = await _tokenService.GenerateRefreshTokenAsync(user.Id);
        var accessTokenRecord = await SetAccessTokenAsync(user);
        return new()
        {
            RefreshToken = refreshTokenRecord.refreshToken,
            AccessToken = accessTokenRecord.accessToken,
            RefreshTokenExpiresAt = refreshTokenRecord.expiresAt,
            AccessTokenExpiresAt = accessTokenRecord.expiresAt
        };
    }
    
    private async Task<AccessTokenRecord> SetAccessTokenAsync(AuthUser user)
    {
        var signingCredentials = _tokenService.GetJwtSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptionsRecord = _tokenService.GenerateJwtTokenOptions(signingCredentials, claims);
        var encodedAccessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptionsRecord.token);
        return new(encodedAccessToken, tokenOptionsRecord.expiresAt);
    }
    
    private async Task<List<Claim>> GetClaims(AuthUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email),
            new ("Id", user.Id.ToString())
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }
}