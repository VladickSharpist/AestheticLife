using Aesthetic.CQRS.Abstractions;
using Aesthetic.CQRS.Abstractions.Models.UserDto;
using Aesthetic.CQRS.Commands;
using MassTransit;
using RabbitMq.Events;

namespace AestheticsLife.User.Service.Consumers;

public class NewUserRegisteredConsumer: IConsumer<NewUserRegistered>
{
    private readonly ICommandExecutor _executor;
    
    public NewUserRegisteredConsumer(ICommandExecutor executor)
    {
        _executor = executor;
    }
    
    public async Task Consume(ConsumeContext<NewUserRegistered> context)
    {
        await _executor.ExecuteAsync<long, UserDto>(
            new AddCommand<UserDto>(new UserDto { Email = context.Message.Email}));
    }
}