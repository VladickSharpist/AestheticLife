using System.ComponentModel.DataAnnotations;

namespace AestheticLife.Web.FileUploader.Models.Request;

public class UploadFileForEntityRequestVm
{
    [Required]
    public string UploadToken { get; set; }
    
    [Required]
    public IFormFile File { get; set; }
}