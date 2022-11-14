using AestheticsLife.File.Services.Abstractions.Interfaces;
using AestheticsLife.File.Services.Abstractions.Models;
using AestheticsLife.FileUploader.Service.Models;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMq;

namespace AestheticsLife.FileUploader.Service.Controllers;

[ApiController]
[Route("/api/fileUploader")]
public class FileUploaderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IBus _bus;
    
    public FileUploaderController(IMapper mapper, IFileService fileService, IBus bus)
    {
        _mapper = mapper;
        _fileService = fileService;
        _bus = bus;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Post([FromQuery] string uploadToken, [FromForm] UploadFileVm model)
    {
        var dto = new UploadFileDto
        {
            UploadToken = uploadToken,
            File = model.File.OpenReadStream()
        };
        var token = await _fileService.UploadFileForEntry(dto);
        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{token.QueueName}"));
        await endpoint.Send(token.Message);
        return Ok();
    }
}