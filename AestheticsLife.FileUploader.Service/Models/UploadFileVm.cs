using System.ComponentModel.DataAnnotations;

namespace AestheticsLife.FileUploader.Service.Models;

public class UploadFileVm
{
    [Required]
    public IFormFile File { get; set; }
}