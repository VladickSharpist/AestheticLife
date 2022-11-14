using AestheticLife.Core.Abstractions.Models;
using AestheticLife.Core.Abstractions.Storages;
using AestheticsLife.File.Services.Abstractions.Interfaces;
using AestheticsLife.File.Services.Abstractions.Models;
using Logic.Shared.Abstractions;
using Logic.Shared.Abstractions.Models;

namespace AestheticsLife.File.Services.Implementations;

internal class FileService : IFileService
{
    private readonly IFileStorage _fileStorage;
    private readonly ITokenService _tokenService;

    public FileService(
        IFileStorage fileStorage,
        ITokenService tokenService)
    {
        _fileStorage = fileStorage;
        _tokenService = tokenService;
    }

    public async Task<UploadToken> UploadFileForEntry(UploadFileDto dto)
    {
        var token = _tokenService.DecodeToken<UploadToken>(dto.UploadToken);
        using (var ms = new MemoryStream())
        {
            await dto.File.CopyToAsync(ms);
            var item = new StorageItem
            {
                File = ms.ToArray(),
                RelativePath = token.Message.FilePath
            };
            var fullPath = await _fileStorage.SaveFileAsync(item);
            token.Message.FilePath = fullPath;
        }

        return token;
    }
}