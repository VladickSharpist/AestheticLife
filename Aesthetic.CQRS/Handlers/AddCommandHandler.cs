using Aesthetic.CQRS.Abstractions;
using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AutoMapper;

namespace Aesthetic.CQRS.Handlers;

public class AddCommandHandler<TCommand, TDto, TEntity>
    : IHandler<TCommand, TDto, long> 
    where TEntity: class, IEntity
    where TCommand: class, ICommand<TDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<long> HandleAsync(TCommand command)
    {
        return await _unitOfWork.GetReadWriteRepository<TEntity>().SaveAsync(_mapper.Map<TEntity>(command.Data));
    }
}