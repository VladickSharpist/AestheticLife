using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Auth.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;


    public UserService(
        UserManager<User> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }


    public async Task<UserDto> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        //role
        return _mapper.Map<UserDto>(user);
    }
}