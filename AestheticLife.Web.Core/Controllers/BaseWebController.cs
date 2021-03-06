using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.Core.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseWebController : ControllerBase
{
    protected readonly IMapper _mapper;

    public BaseWebController(IMapper mapper)
    {
        _mapper = mapper;
    }
}