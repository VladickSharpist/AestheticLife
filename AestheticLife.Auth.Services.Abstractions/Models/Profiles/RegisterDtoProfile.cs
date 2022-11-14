using AutoMapper;
using DataAccess.Auth.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Models.Profiles;

public class RegisterDtoProfile : Profile
{
    public RegisterDtoProfile()
    {
        CreateMap<RegisterUserDto, AuthUser>().ReverseMap();
    }
}