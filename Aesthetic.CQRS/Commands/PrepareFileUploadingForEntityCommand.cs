using Aesthetic.CQRS.Abstractions;

namespace Aesthetic.CQRS.Commands;

public class PrepareFileUploadingForEntityCommand<TDto>: ICommand<TDto>
{
    public PrepareFileUploadingForEntityCommand(TDto data)
    {
        Data = data;
    }
    public TDto Data { get; set; }
}