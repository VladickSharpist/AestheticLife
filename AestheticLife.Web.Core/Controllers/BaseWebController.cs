using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.Core.Controllers;

public class BaseWebController : ControllerBase
{
    protected readonly IMapper _mapper;

    public BaseWebController(IMapper mapper)
    {
        _mapper = mapper;
    }
}