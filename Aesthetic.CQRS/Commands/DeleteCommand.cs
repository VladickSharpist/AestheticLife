using Aesthetic.CQRS.Abstractions;

namespace Aesthetic.CQRS.Commands;

public class DeleteCommand: ICommand<long>
{
    public DeleteCommand(long id)
    {
        Data = id;
    }
    
    public long Data { get; set; }
}