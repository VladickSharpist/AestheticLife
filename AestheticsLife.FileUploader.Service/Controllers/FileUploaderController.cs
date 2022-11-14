using AestheticsLife.File.Services.Abstractions.Interfaces;
using AestheticsLife.File.Services.Abstractions.Models;
using AestheticsLife.FileUploader.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AestheticsLife.FileUploader.Service.Controllers;

[ApiController]
[Route("/api/fileUploader")]
public class FileUploaderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    
    public FileUploaderController(IMapper mapper, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFileForEntity([FromForm] UploadFileVm model)
    {
        await _fileService.UploadFileForEntry(_mapper.Map<UploadFileDto>(model));
        return Ok();
    }
}