using AestheticsLife.DataAccess.Training.Abstractions.Models;
using AutoMapper;

namespace Aesthetic.CQRS.Abstractions.Models.ExerciseDto;

public class ExerciseProfile: Profile
{
    public ExerciseProfile()
    {
        CreateMap<ExerciseDto, Exercise>().ReverseMap();
    }
}