using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Back_Abode.Models;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;
    private readonly RegistrationService _registrationService;

    public UserController(AuthenticationService authenticationService, RegistrationService registrationService)
    {
        _authenticationService = authenticationService;
        _registrationService = registrationService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> IniciarSesion([FromBody] Users request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.Authenticate(request.email, request.password);

            if (result.success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "Ocurrió un error durante la autenticación.",
                result = ex.Message
            });
        }
    }
}
