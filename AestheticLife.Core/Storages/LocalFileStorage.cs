using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.Core.FileStorage.Abstractions.Interfaces;
using AestheticLife.Core.FileStorage.Abstractions.Models;

namespace AestheticLife.Core.FileStorage.Implementations;

internal class LocalFileStorage : IFileStorage
{
    private readonly string _envPath;

    public LocalFileStorage(IConfigurationHelper helper)
    {
        _envPath = helper.LocalStorageAbsolutePath;
    }

    public async Task<string> SaveFileAsync(StorageItem item)
    {
        var fullPath = $"{_envPath}\\{item.RelativePath}";
        var fileStream = File.Create(fullPath);
        await fileStream.WriteAsync(item.File);
        return fullPath;
    }

    public string GetFileLink(string relativePath)
    {
        return $"{_envPath}\\{relativePath}";
    }
}