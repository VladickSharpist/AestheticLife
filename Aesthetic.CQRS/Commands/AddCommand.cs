using Aesthetic.CQRS.Abstractions;

namespace Aesthetic.CQRS.Commands;

public class AddCommand<TDto>: ICommand<TDto>
{
    public AddCommand(TDto data)
    {
        Data = data;
    }
    public TDto Data { get; set; }
}