namespace Aesthetic.CQRS.Abstractions;

public interface IHandler<TCommand, TData, TReturnType>
    where TCommand: ICommand<TData>
{
    public Task<TReturnType> HandleAsync(TCommand command);
}