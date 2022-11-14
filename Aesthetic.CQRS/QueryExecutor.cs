using Aesthetic.CQRS.Abstractions;
using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AutoMapper;

namespace Aesthetic.CQRS;

public class QueryExecutor<TDto, TEntity>: IQueryExecutor<TDto>
    where TEntity: class ,IEntity
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public QueryExecutor(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TReturnType> ExecuteAsync<TReturnType>() 
        where TReturnType : class, IEnumerable<TDto>
    {
        return _mapper
            .Map<TReturnType>(
                await _unitOfWork.GetReadonlyRepository<TEntity>().GetNoTrackingAsync());
    }

    public async Task<TReturnType> ExecuteAsync<TReturnType>(IQuery query) 
        where TReturnType : class
    {
        return _mapper
            .Map<TReturnType>(
                await _unitOfWork.GetReadonlyRepository<TEntity>()
                    .SingleOrDefaultAsync(e => e.Id == query.Id));
    }
}