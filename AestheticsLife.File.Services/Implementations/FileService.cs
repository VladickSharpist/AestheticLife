using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Core.Abstractions.Models;
using AestheticLife.Core.Abstractions.Storages;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AestheticsLife.File.Services.Abstractions.Interfaces;
using AestheticsLife.File.Services.Abstractions.Models;
using AutoMapper;

namespace AestheticsLife.File.Services.Implementations;

internal class FileService : IFileService
{
    private readonly IFileStorage _fileStorage;
    // private readonly IUnitOfWork _unitOfWork;
    // private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public FileService(IFileStorage fileStorage, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        _fileStorage = fileStorage;
        // _unitOfWork = unitOfWork;
        // _mapper = mapper;
        _tokenService = tokenService;
    }

    // public async Task<string> CreateFileEntity<TEntityWithFile>(FileEntryDto dto)
    //     where TEntityWithFile: class, IEntityWithFile
    // {
    //     var entityRepo = _unitOfWork
    //         .GetReadWriteRepository<TEntityWithFile>();
    //     var entity = (await entityRepo
    //         .GetAsync(m => m.Id == dto.EntityId)).FirstOrDefault();
    //     
    //     if (entity is null) 
    //         throw new Exception($"Entity {typeof(TEntityWithFile)} with {dto.EntityId} doesnt exist");
    //
    //     var fileEntity = _mapper.Map<AestheticLife.DataAccess.Domain.Models.File>(dto);
    //     await _unitOfWork
    //         .GetReadWriteRepository<AestheticLife.DataAccess.Domain.Models.File>()
    //         .SaveAsync(fileEntity);
    //     
    //     entity.FileId = fileEntity.Id;
    //     await entityRepo.SaveAsync(entity);
    //     
    //     return _tokenService.EncodeToken(_mapper.Map<FileUploadTokenDto>(fileEntity));
    // }

    public async Task UploadFileForEntry(UploadFileDto dto)
    {
        var decodedToken = _tokenService.DecodeToken<FileUploadTokenDto>(dto.UploadToken);
        using (var ms = new MemoryStream())
        {
            await dto.File.CopyToAsync(ms);
            var item = new StorageItem
            {
                File = ms.ToArray(),
                RelativePath = decodedToken.RelativePath
            };
            await _fileStorage.SaveFileAsync(item);
        }
    }
}