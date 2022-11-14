namespace Logic.Shared.Abstractions;

public interface ICurrentUserGetter
{
    public long CurrentUserId { get; }
}