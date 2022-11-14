namespace Aesthetic.CQRS.Abstractions;

public interface IQueryExecutor<TDto>
{
    public Task<TReturnType> ExecuteAsync<TReturnType>(IQuery query)
        where TReturnType : class;
    
    public Task<TReturnType> ExecuteAsync<TReturnType>()
        where TReturnType : class, IEnumerable<TDto>;
}