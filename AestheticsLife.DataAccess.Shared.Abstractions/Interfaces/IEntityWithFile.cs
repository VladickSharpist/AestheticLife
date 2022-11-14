namespace AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;

public interface IEntityWithFile : IEntity
{
    public string? FilePath { get; set; }
}