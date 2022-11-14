namespace Aesthetic.CQRS.Abstractions;

public interface ICommandExecutor
{
    public Task<TReturnType> ExecuteAsync<TReturnType, TData>(ICommand<TData> command);
}