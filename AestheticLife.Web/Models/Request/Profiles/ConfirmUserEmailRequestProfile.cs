using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models.Request.Profiles;

public class ConfirmUserEmailRequestProfile : Profile
{
    public ConfirmUserEmailRequestProfile()
    {
        CreateMap<ConfirmUserEmailRequestVm, ConfirmUserEmailDto>();
    }
}