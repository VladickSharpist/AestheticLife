using Aesthetic.CQRS.Abstractions.Models.ExerciseDto;
using AutoMapper;

namespace AestheticsLife.Training.Service.Models.Profiles;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<ExerciseVm, ExerciseDto>().ReverseMap();
        CreateMap<AddExerciseVm, ExerciseDto>().ReverseMap();
    }
}