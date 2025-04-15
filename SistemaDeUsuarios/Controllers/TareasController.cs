using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeUsuarios.DTOs.Tareas;
using SistemaDeUsuarios.Interfaces;
using SistemaDeUsuarios.Models;

namespace SistemaDeUsuarios.Controllers;

[ApiController]
[Route("api/tareas")]
public class TareasController : Controller
{
   private readonly ITareasServicios _tareasServicios;

   public TareasController(ITareasServicios tareasServicios)
   {
      _tareasServicios = tareasServicios;
   }

   [HttpPost]
   [Authorize (Roles = "Maestro")]
   public async Task<IActionResult> CrearTareaAsync([FromBody] CrearTareasDTos tarea)
   {
       var resultado = await _tareasServicios.CrearTareasAsync(tarea);
       if(resultado.Success)
           return Ok(resultado);
       
       return BadRequest(resultado);
   }

   [HttpGet("{tareaId}")]
   [Authorize]
   public async Task<IActionResult> ObtenerTareaIdAsync(Guid tareaId)
   {
       var resultado = await _tareasServicios.ObtenerTareasId(tareaId);
       if(resultado.Success)
           return Ok(resultado);
       
       return NotFound(resultado);
   }

   [HttpGet]
   [Authorize]
   public async Task<IActionResult> ObtenerTareasAsync()
   {
       var resultado = await _tareasServicios.ListarTareasAsync();
       if(resultado.Success)
           return Ok(resultado);
       
       return NotFound(resultado);
   }
   
   [HttpPut("{tareaId}")]
   [Authorize (Roles = "Maestro")]
   public async Task<IActionResult> ActualizarTareasAsync([FromRoute] Guid tareaId,[FromBody] ActualizarTareasDTos tarea)
   {
       var resultado = await _tareasServicios.ActualizarTareasAsync(tareaId,tarea);
       if(resultado.Success)
           return Ok(resultado);
       
       return NotFound(resultado);
   }
   
   [HttpDelete("{tareaId}")]
   [Authorize (Roles = "Maestro")]
   public async Task<IActionResult> BorrarTareasAsync(Guid tareaId)
   {
       var resultado = await _tareasServicios.BorrarTareasAsync(tareaId);
       if(resultado.Success)
           return Ok(resultado);
       
       return NotFound(resultado);
   }
   
}