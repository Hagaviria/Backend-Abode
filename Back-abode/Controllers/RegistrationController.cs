using Back_Abode.Models;
using Back_Proyecto.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("user")]
public class RegistrationController : ControllerBase
{
    private readonly RegistrationService _registrationService;

    public RegistrationController(RegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Registrar([FromBody] Registro registro)
    {
        try
        {
            var result = await _registrationService.Register(registro);

            if (result.Success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    result = result.Result
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message,
                    result = result.Result
                });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "Ocurrió un error durante el registro.",
                result = ex.Message
            });
        }
    }
}
