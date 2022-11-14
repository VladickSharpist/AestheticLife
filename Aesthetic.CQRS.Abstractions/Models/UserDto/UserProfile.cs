using AestheticsLife.DataAccess.User.Abstractions.Models;
using AutoMapper;

namespace Aesthetic.CQRS.Abstractions.Models.UserDto;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, ApplicationUser>().ReverseMap();
    }
}