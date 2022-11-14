using Aesthetic.CQRS.Abstractions.Models.UserDto;
using AutoMapper;

namespace AestheticsLife.User.Service.Models.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserVm, UserDto>().ReverseMap();
    }
}