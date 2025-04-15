using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeUsuarios.DTOs.Folder;
using SistemaDeUsuarios.Interfaces;

namespace SistemaDeUsuarios.Controllers;

[ApiController]
[Route("api/folder")]
[Authorize (Roles = "Maestro")]
public class FolderController : Controller
{
    private readonly IFolderServicio _servicio;
    public FolderController(IFolderServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpPost]
    public async Task<IActionResult> CrearFolderAsync([FromBody] CrearActualizarFolderDTos folderDto)
    {
        var resultado = await _servicio.CrearFolderAsync(folderDto);
        if(resultado.Success)
            return Ok(resultado);
        
        return BadRequest(resultado);
    }
    
    [HttpGet("{folderId}")]
    public async Task<IActionResult> ObtenerFolderId(Guid folderId)
    {
        var resultado = await _servicio.ObtenerFolderId(folderId);
        if(resultado.Success)
            return Ok(resultado);
        
        return BadRequest(resultado);
    }
    
    [HttpPut("{folderId}")]
    public async Task<IActionResult> ActualizarFolderAsync([FromRoute] Guid folderId,[FromBody] CrearActualizarFolderDTos folderDto)
    {
        var resultado = await _servicio.ActualizarFolderAsync(folderId, folderDto);
        if(resultado.Success)
            return Ok(resultado);
        
        return BadRequest(resultado);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListarFoldersAsync()
    {
        var resultado = await _servicio.ListarFoldersAsync();
        if(resultado.Success)
            return Ok(resultado);
        
        return BadRequest(resultado);
    }
    
    [HttpDelete("{folderId}")]
    public async Task<IActionResult> BorrarFolderAsync(Guid folderId)
    {
        var resultado = await _servicio.BorrarFolderAsync(folderId);
        if(resultado.Success)
            return Ok(resultado);
        
        return BadRequest(resultado);
    }
}