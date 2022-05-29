using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;

namespace AestheticLife.DataAccess.Domain.Abstractions.Models;

public abstract class BaseEntity: IEntity
{
    public long Id { get; set; }
}