using AestheticsLife.File.Services.Abstractions.Models;
using Logic.Shared.Abstractions.Models;

namespace AestheticsLife.File.Services.Abstractions.Interfaces;

public interface IFileService
{
    public Task<UploadToken> UploadFileForEntry(UploadFileDto dto);
}