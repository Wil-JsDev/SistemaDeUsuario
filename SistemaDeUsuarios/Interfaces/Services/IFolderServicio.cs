using SistemaDeUsuarios.DTOs.Folder;
using SistemaDeUsuarios.Utils;

namespace SistemaDeUsuarios.Interfaces;

public interface IFolderServicio
{
    Task<ApiResponse<FolderDTos>> CrearFolderAsync(CrearActualizarFolderDTos crearFolderDTos);
    
    Task<ApiResponse<FolderDTos>> ActualizarFolderAsync(Guid folderId,CrearActualizarFolderDTos actualizarFolderDTos);
    
    Task<ApiResponse<Guid>> BorrarFolderAsync(Guid folderId);
    
    Task<ApiResponse<IEnumerable<FolderDTos>>> ListarFoldersAsync();
    
    Task<ApiResponse<FolderDTos>> ObtenerFolderId(Guid folderId);
}