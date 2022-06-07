using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models2.Request.Profiles;

public class ConfirmUserEmailRequestProfile : Profile
{
    public ConfirmUserEmailRequestProfile()
    {
        CreateMap<ConfirmUserEmailRequestVm, ConfirmUserEmailDto>();
    }
}