using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;
using Logic.Shared.Abstractions.Models;

namespace Auth.Service.Models.Profiles;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginRequestVm, LoginDto>();
        CreateMap<TokenDto, TokenResponseVm>();
        CreateMap<TokenDto, RefreshResponseVm>();
    }
}