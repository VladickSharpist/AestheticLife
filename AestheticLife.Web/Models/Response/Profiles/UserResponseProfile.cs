using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models.Response.Profiles;

public class UserResponseProfile : Profile
{
    public UserResponseProfile()
    {
        CreateMap<UserDto, CurrentUserResponseVm>();
    }
}