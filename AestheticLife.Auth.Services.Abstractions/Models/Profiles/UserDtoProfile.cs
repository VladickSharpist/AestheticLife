using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;

namespace AestheticLife.Auth.Services.Abstractions.Models.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<User, UserDto>();
    }
}