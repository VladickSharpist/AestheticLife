namespace Aesthetic.CQRS.Abstractions;

public interface ICommand<TData>
{
    public TData Data { get; set; }
}