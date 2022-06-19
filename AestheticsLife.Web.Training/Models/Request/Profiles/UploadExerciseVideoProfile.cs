using AestheticsLife.Training.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticsLife.Web.Training.Models.Request.Profiles;

public class UploadExerciseVideoProfile : Profile
{
    public UploadExerciseVideoProfile()
    {
        CreateMap<UploadExerciseVideoRequestVm, UploadFileRequestDto>();
    }
}