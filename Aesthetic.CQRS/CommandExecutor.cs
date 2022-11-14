using Aesthetic.CQRS.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aesthetic.CQRS;

public class CommandExecutor: ICommandExecutor
{
    private readonly IServiceProvider _provider;
    public CommandExecutor(IServiceProvider provider)
    {
        _provider = provider;
    }
    public async Task<TReturnType> ExecuteAsync<TReturnType, TData>(ICommand<TData> command)
    {
        var handler = _provider.GetService<IHandler<ICommand<TData>, TData, TReturnType>>();
        if (handler is null) throw new Exception("Handler unresolved or doesnt exist");
        return await handler.HandleAsync(command);
    }
}