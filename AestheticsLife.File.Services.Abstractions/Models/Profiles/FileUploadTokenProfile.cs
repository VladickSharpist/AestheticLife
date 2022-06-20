using AutoMapper;

namespace AestheticsLife.File.Services.Abstractions.Models.Profiles;

public class FileUploadTokenProfile : Profile
{
    public FileUploadTokenProfile()
    {
        CreateMap<AestheticLife.DataAccess.Domain.Models.File, FileUploadTokenDto>()
            .ForMember(dto => dto.FileId, opt => 
                opt.MapFrom(db => db.Id))
            .ForMember(dto => dto.RelativePath, opt =>
                opt.MapFrom(m => $"{m.RelativePath}\\{m.Name}.{m.Extension}"));
    }
}