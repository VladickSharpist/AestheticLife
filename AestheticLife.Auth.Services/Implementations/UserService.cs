using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AestheticsLife.DataAccess.User.Abstractions.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Auth.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserSetter _userSetter;


    public UserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IUserSetter userSetter)
    {
        _userManager = userManager;
        _mapper = mapper;
        _userSetter = userSetter;
    }


    public async Task SetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var userDto = _mapper.Map<UserDto>(user);
        _userSetter.User = userDto;
    }
}