using SistemaDeUsuarios.DTOs.Folder;
using SistemaDeUsuarios.DTOs.Tareas;
using SistemaDeUsuarios.Utils;

namespace SistemaDeUsuarios.Interfaces;

public interface ITareasServicios
{
    Task<ApiResponse<TareasDTos>> CrearTareasAsync(CrearTareasDTos crearTareasDTos);
    
    Task<ApiResponse<TareasDTos>> ActualizarTareasAsync(Guid tareasId,ActualizarTareasDTos actualizarTareasDTos);
    
    Task<ApiResponse<Guid>> BorrarTareasAsync(Guid tareasId);
    
    Task<ApiResponse<IEnumerable<TareasDTos>>> ListarTareasAsync();
    
    Task<ApiResponse<TareasDTos>> ObtenerTareasId(Guid folderId);
}