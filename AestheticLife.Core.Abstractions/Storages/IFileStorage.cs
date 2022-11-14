using AestheticLife.Core.Abstractions.Models;

namespace AestheticLife.Core.Abstractions.Storages;

public interface IFileStorage
{
    public Task<string> SaveFileAsync(StorageItem item);

    public string GetFileLink(string relativePath);
}