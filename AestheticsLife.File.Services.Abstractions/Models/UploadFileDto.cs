namespace AestheticsLife.File.Services.Abstractions.Models;

public class UploadFileDto
{
    public string UploadToken { get; set; }

    public Stream File { get; set; }
}