using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models.Response.Profiles;

public class CurrentUserResponseProfile : Profile
{
    public CurrentUserResponseProfile()
    {
        CreateMap<UserDto, CurrentUserResponseVm>();
    }
}