using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;

namespace AestheticLife.DataAccess.Domain.Abstractions.Models;

public class EntityWithFile : BaseEntity, IEntityWithFile
{
    public long? FileId { get; set; }
}