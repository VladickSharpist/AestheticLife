using AestheticLife.Web.Core;
using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : BaseWebController
{

    public AccountController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> Hello() => new OkObjectResult("Hello");
}