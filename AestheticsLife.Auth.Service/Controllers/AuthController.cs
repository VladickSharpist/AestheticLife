namespace AestheticsLife.Auth.Service.Controllers;

public class AuthController
{
    [HttpPost]
    public async Task<ActionResult<bool>> Registration([FromBody] RegistrationRequestVm model)
        => new (await _authService.RegisterAsync(
            _mapper.Map<RegisterUserDto>(model)));
}