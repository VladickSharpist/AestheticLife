using Aesthetic.CQRS.Abstractions;
using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AutoMapper;

namespace Aesthetic.CQRS.Handlers;

public class UpdateCommandHandler<TCommand, TDto, TEntity>
    : IHandler<TCommand, TDto, TDto>
    where TCommand: class, ICommand<TDto>
    where TEntity: class, IEntity
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<TDto> HandleAsync(TCommand command)
    {
        var updatedModel = await _unitOfWork.GetReadWriteRepository<TEntity>().UpdateAsync(_mapper.Map<TEntity>(command.Data));
        return _mapper.Map<TDto>(updatedModel);
    }
}