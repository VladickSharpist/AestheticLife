using AestheticLife.DataAccess.Domain.Models;
using AutoMapper;

namespace AestheticsLife.Training.Services.Abstractions.Models.Profiles;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<ExerciseDto, Exercise>();
    }
}