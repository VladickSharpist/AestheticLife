using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models.Request.Profiles;

public class RegistrationRequestProfile : Profile
{
    public RegistrationRequestProfile()
    {
        CreateMap<RegistrationRequestVm, RegisterUserDto>()
            .ForMember(dto => dto.DateOfBirth, opt =>
                opt.MapFrom(vm => DateTime.Parse(vm.DateOfBirth)));
    }
}