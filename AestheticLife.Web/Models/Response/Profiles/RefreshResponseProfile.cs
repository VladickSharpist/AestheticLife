using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.Models.Response.Profiles;

public class RefreshResponseProfile : Profile
{
    public RefreshResponseProfile()
    {
        CreateMap<TokenDto, RefreshResponseVm>();
    }
}