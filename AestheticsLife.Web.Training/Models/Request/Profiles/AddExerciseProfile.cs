using AestheticsLife.Training.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticsLife.Web.Training.Models.Request.Profiles;

public class AddExerciseProfile : Profile
{
    public AddExerciseProfile()
    {
        CreateMap<AddExerciseRequestVm, ExerciseDto>();
    }
}