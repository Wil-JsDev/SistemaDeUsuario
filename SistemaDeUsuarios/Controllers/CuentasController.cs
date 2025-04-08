using Microsoft.AspNetCore.Mvc;
using SistemaDeUsuarios.DTOs;
using SistemaDeUsuarios.Enum;
using SistemaDeUsuarios.Interfaces;
using SistemaDeUsuarios.Utils;
using RegisterRequest = SistemaDeUsuarios.DTOs.RegisterRequest;

namespace SistemaDeUsuarios.Controllers;

[ApiController]
[Route("api/cuentas")]
public class CuentasController(IServicioDeCuentas servicioDeCuentas) : ControllerBase
{

    [HttpPost("registrar-estudiante")]
    public async Task<IActionResult> CrearEstudianteAsync([FromBody] RegisterRequest registerRequest)
    {
        var resultado = await servicioDeCuentas.RegisterAccountAsync(registerRequest,Roles.Estudiante);
        if (resultado.Success)
            return Ok(resultado.Data);
        
        return NotFound(resultado.ErrorMessage);
    }
    
    [HttpPost("registrar-maestro")]
    public async Task<IActionResult> CrearMaestroAsync([FromBody] RegisterRequest registerRequest)
    {
        var resultado = await servicioDeCuentas.RegisterAccountAsync(registerRequest,Roles.Maestro);
        if (resultado.Success)
            return Ok(resultado.Data);
        
        return NotFound(resultado.ErrorMessage);
    }

    [HttpPost("autenticar")]
    public async Task<IActionResult> AutenticarAsync([FromBody] AuthenticateRequest loginRequest)
    {
        var result = await servicioDeCuentas.AuthenticateAsync(loginRequest);
        return result.StatusCodes switch
        {
            404 => NotFound(ApiResponse<string>.ErrorResponse($" Email {loginRequest.Email} no encontrado")),
            400 => BadRequest(ApiResponse<string>.ErrorResponse($"Cuenta no confirmada con este email {loginRequest.Email}")),
            401 => Unauthorized(ApiResponse<string>.ErrorResponse($"Credenciales no validas para el Email {loginRequest.Email}")),
            _ => Ok(ApiResponse<AuthenticateResponse>.SuccessResponse(result))
        };
    }
}