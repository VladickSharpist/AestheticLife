using RabbitMq;

namespace AestheticsLife.File.Services.Abstractions.Models;

public class FileUploadTokenDto
{
    public long FileId { get; set; }

    public string RelativePath { get; set; }
}