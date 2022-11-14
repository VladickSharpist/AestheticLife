namespace AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;

public interface IEntityWithFile : IEntity
{
    string? FilePath { get; set; }
    bool IsDataReady { get; set; }
    string? FileName { get; set; }
}