using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.Core.Abstractions.Models;
using AestheticLife.Core.Abstractions.Storages;

namespace AestheticsLife.Core.Storages;

internal class LocalFileStorage : IFileStorage
{
    private readonly string _envPath;
    private readonly string _webLink;

    public LocalFileStorage(IConfigurationHelper helper)
    {
        _envPath = helper.LocalStoragePath;
        _webLink = helper.WebStorageAccessLink;
    }

    public async Task<string> SaveFileAsync(StorageItem item)
    {
        var fullPath = $"{_envPath}\\{item.RelativePath}";
        await using var fileStream = File.Create(fullPath);
        await fileStream.WriteAsync(item.File);
        return $"{_webLink}/{item.RelativePath}";
    }

    public string GetFileLink(string relativePath)
    {
        return $"{_webLink}/{relativePath}";
    }
}