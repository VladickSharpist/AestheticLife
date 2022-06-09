using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models2.Request.Profiles;

public class RegistrationRequestProfile : Profile
{
    public RegistrationRequestProfile()
    {
        CreateMap<RegistrationRequestVm, RegisterUserDto>();
    }
}