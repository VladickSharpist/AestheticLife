namespace AestheticsLife.File.Services.Abstractions.Models;

public class FileEntryDto
{
    public long EntityId { get; set; }
    
    public string RelativePath { get; set; }

    public string Name { get; set; }

    public string Extension { get; set; }

    public double? Duration { get; set; }

    public string? Hash { get; set; }
}