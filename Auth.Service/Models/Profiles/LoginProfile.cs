using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace Auth.Service.Models.Profiles;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginRequestVm, LoginDto>();
        CreateMap<TokenDto, LoginResponseVm>();
        CreateMap<TokenDto, RefreshResponseVm>();
    }
}