namespace Logic.Shared.Abstractions;

public interface ICurrentUserSetter
{
    public long CurrentUserId { set; }
}