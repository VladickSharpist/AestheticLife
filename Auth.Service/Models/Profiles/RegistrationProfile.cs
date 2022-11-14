using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace Auth.Service.Models.Profiles;

public class RegistrationProfile : Profile
{
    public RegistrationProfile()
    {
        CreateMap<RegistrationVm, RegisterUserDto>();
    }
}