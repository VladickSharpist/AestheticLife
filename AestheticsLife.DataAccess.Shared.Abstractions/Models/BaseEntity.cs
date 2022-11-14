using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;

namespace AestheticsLife.DataAccess.Shared.Abstractions.Models;

public abstract class BaseEntity: IEntity
{
    public long Id { get; set; }
}