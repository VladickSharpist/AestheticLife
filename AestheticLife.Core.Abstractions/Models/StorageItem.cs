namespace AestheticLife.Core.FileStorage.Abstractions.Models;

public class StorageItem
{
    public string RelativePath { get; set; }

    public byte[] File { get; set; }
}