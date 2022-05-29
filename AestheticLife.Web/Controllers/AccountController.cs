using Microsoft.AspNetCore.Mvc;

namespace AestheticLife.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{

    public AccountController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> Hello() => new OkObjectResult("Hello");
}