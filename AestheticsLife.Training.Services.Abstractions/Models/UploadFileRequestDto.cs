namespace AestheticsLife.Training.Services.Abstractions.Models;

public class UploadFileRequestDto
{
    public long EntityId { get; set; }
    
    public string RelativePath { get; set; }

    public string Name { get; set; }

    public string Extension { get; set; }
}