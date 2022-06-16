using AutoMapper;

namespace AestheticsLife.File.Services.Abstractions.Models.Profiles;

public class FileEntryProfile : Profile
{
    public FileEntryProfile()
    {
        CreateMap<FileEntryDto, AestheticLife.DataAccess.Domain.Models.File>();
    }
}