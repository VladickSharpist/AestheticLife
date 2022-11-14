using Aesthetic.CQRS.Abstractions;
using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AutoMapper;
using Logic.Shared.Abstractions;
using Logic.Shared.Abstractions.Models;
using RabbitMq;

namespace Aesthetic.CQRS.Handlers;

public class PrepareFileUploadingForEntityCommandHandler<TCommand, TDto, TEntity, TMessage>
    : IHandler<TCommand, TDto, string>
    where TCommand: class, ICommand<TDto>
    where TEntity: class, IEntityWithFile, new()
    where TDto : class, IFileWithData
    where TMessage : UploadedFileReadyEvent, new()
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    public PrepareFileUploadingForEntityCommandHandler(
        IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    public async Task<string> HandleAsync(TCommand command)
    {
        var entity = await _unitOfWork.GetReadonlyRepository<TEntity>()
            .SingleOrDefaultAsync(e => e.Id == command.Data.Id);
        entity.IsDataReady = false;
        entity.FilePath = Guid.NewGuid() + $"-{command.Data.FileName}";
        entity.FileName = command.Data.FileName;
        await _unitOfWork.SaveChangesAsync();
        var token = _tokenService.EncodeToken(new UploadToken
        {
            Message = new TMessage
            {
                Id = entity.Id,
                FilePath = entity.FilePath
            },
            QueueName = typeof(TMessage).Name
        });
        return token;
    }
}