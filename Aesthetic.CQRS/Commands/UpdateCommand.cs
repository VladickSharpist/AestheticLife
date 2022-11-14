using Aesthetic.CQRS.Abstractions;

namespace Aesthetic.CQRS.Commands;

public class UpdateCommand<TDto>: ICommand<TDto>
{
    public UpdateCommand(TDto data)
    {
        Data = data;
    }
    public TDto Data { get; set; }
}