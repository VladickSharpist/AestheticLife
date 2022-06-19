using AestheticLife.Web.Core.Controllers;
using AestheticLife.Web.FileUploader.Models.Request;
using AestheticsLife.File.Services.Abstractions;
using AestheticsLife.File.Services.Abstractions.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.FileUploader.Controllers;

public class FileUploaderController : BaseWebController
{
    private readonly IFileService _fileService;
    
    public FileUploaderController(IMapper mapper, IFileService fileService) : base(mapper)
    {
        _fileService = fileService;
    }

    public async Task<IActionResult> UploadFileForEntity([FromForm] UploadFileForEntityRequestVm model)
    {
        await _fileService.UploadFileForEntry(_mapper.Map<UploadFileDto>(model));
        return Ok();
    }
}