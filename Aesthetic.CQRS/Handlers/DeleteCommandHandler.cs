using Aesthetic.CQRS.Abstractions;
using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;

namespace Aesthetic.CQRS.Handlers;

public class DeleteCommandHandler<TCommand, TEntity>
    : IHandler<TCommand, long, bool>
    where TCommand: class, ICommand<long>
    where TEntity: class, IEntity
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> HandleAsync(TCommand command)
    { 
        await _unitOfWork.GetReadWriteRepository<TEntity>().RemoveAsync(command.Data);
        return true;
    }
}