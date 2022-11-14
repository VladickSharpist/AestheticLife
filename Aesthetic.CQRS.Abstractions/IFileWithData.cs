namespace Aesthetic.CQRS.Abstractions;

public interface IFileWithData
{
    long Id { get; set; }
    
    string? FileName { get; set; }

    string? FilePath { get; set; }

    bool IsDataReady { get; set; }
}