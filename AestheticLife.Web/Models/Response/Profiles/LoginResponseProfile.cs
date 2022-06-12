using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models.Response.Profiles;

public class LoginResponseProfile : Profile
{
    public LoginResponseProfile()
    {
        CreateMap<TokenDto, LoginResponseVm>();
    }
}