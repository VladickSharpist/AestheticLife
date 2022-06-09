using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;

namespace AestheticLife.Auth.Services.Abstractions.Models.Profiles;

public class RegisterDtoProfile : Profile
{
    public RegisterDtoProfile()
    {
        CreateMap<RegisterUserDto, User>();
    }
}