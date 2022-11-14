using Logic.Shared.Abstractions;

namespace Logic.Shared;

public class CurrentUser: ICurrentUserGetter, ICurrentUserSetter
{
    public long CurrentUserId { get; set; }
}