using AutoMapper;
using DataAccess.Auth.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Models.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<AuthUser, UserDto>().ReverseMap();
    }
}