using AestheticsLife.File.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticLife.Web.FileUploader.Models.Request.Profiles;

public class UploadFileForEntityRequestProfile : Profile
{
    public UploadFileForEntityRequestProfile()
    {
        CreateMap<UploadFileForEntityRequestVm, UploadFileDto>()
            .ForMember(dto => dto.File, opt => 
                opt.MapFrom(vm => vm.File.OpenReadStream()));
    }
}