using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AestheticsLife.DataAccess.User.Abstractions.Models;
using AutoMapper;
using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Auth.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserSetter _userSetter;


    public UserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IUserSetter userSetter)
    {
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userSetter = userSetter;
    }


    public async Task SetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roleNames = (await _unitOfWork
            .GetCustomRepository<UserRole, IUserRoleRepository>()
            .GetAsync(ur=>ur.User.Id == user.Id, null, ur=>ur.Role))
            .Select(ur => ur.Role.Name);
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = roleNames;
        _userSetter.User = userDto;
    }
}