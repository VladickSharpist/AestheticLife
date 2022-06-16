using AestheticsLife.File.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticsLife.Training.Services.Abstractions.Models.Profiles;

public class UploadFileRequestProfile : Profile
{
    public UploadFileRequestProfile()
    {
        CreateMap<UploadFileRequestDto, FileEntryDto>();
    }
}