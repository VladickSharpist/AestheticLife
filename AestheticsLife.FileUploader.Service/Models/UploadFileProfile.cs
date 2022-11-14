using AestheticsLife.File.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticsLife.FileUploader.Service.Models;

public class UploadFileProfile: Profile
{
    public UploadFileProfile()
    {
        CreateMap<UploadFileVm, UploadFileDto>().ReverseMap();
    }
}