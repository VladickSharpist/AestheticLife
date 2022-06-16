using AestheticLife.Core.FileStorage.Abstractions.Models;

namespace AestheticLife.Core.FileStorage.Abstractions.Interfaces;

public interface IFileStorage
{
    public Task<string> SaveFileAsync(StorageItem item);

    public string GetFileLink(string relativePath);
}