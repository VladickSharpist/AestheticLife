using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.File.Services.Abstractions.Models;

namespace AestheticsLife.File.Services.Abstractions.Interfaces;

public interface IFileService
{
    // public Task<string> CreateFileEntity<TEntityWithFile>(FileEntryDto dto)
    //     where TEntityWithFile: class, IEntityWithFile;

    public Task UploadFileForEntry(UploadFileDto dto);
}