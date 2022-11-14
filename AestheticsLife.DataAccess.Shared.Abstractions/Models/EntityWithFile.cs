using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;

namespace AestheticsLife.DataAccess.Shared.Abstractions.Models;

public class EntityWithFile : BaseEntity, IEntityWithFile
{
    public string? FilePath { get; set; }

    public string? FileName { get; set; }

    public bool IsDataReady { get; set; }
}