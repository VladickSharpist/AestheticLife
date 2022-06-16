using AestheticLife.DataAccess.Domain.Abstractions.Models;

namespace AestheticLife.DataAccess.Domain.Abstractions.Interfaces;

public interface IEntityWithFile : IEntity
{
    public long FileId { get; set; }
}