using SistemaDeUsuarios.DTOs.Folder;
using SistemaDeUsuarios.Interfaces;
using SistemaDeUsuarios.Interfaces.Repository;
using SistemaDeUsuarios.Models;
using SistemaDeUsuarios.Utils;

namespace SistemaDeUsuarios.Services;

public class FolderServicios(IFolderRepository folderRepository) : IFolderServicio
{
    public async Task<ApiResponse<FolderDTos>> CrearFolderAsync(CrearActualizarFolderDTos crearFolderDTos)
    {
        if (crearFolderDTos == null)
            return ApiResponse<FolderDTos>.ErrorResponse("No se puede ser nula la información");

        Folder folder = new()
        {
            FolderId = Guid.NewGuid(),
            Name = crearFolderDTos.Name
        };
        
        await folderRepository.CreateAsync(folder);

        FolderDTos folderDTos = new(FolderId: folder.FolderId, Name: crearFolderDTos.Name);
        
        return ApiResponse<FolderDTos>.SuccessResponse(folderDTos);
    }

    public async Task<ApiResponse<FolderDTos>> ActualizarFolderAsync(Guid folderId,CrearActualizarFolderDTos actualizarFolderDTos)
    {
        var folder = await folderRepository.GetByIdAsync(folderId);
        if (folder == null)
            return ApiResponse<FolderDTos>.ErrorResponse($"{folderId} folder no encontrado");

        folder.Name = actualizarFolderDTos.Name;
        
        await folderRepository.UpdateAsync(folder);

        FolderDTos folderDTos = new(FolderId: folder.FolderId, Name: actualizarFolderDTos.Name);
        
        return ApiResponse<FolderDTos>.SuccessResponse(folderDTos);
    }

    public async Task<ApiResponse<Guid>> BorrarFolderAsync(Guid folderId)
    {
       var folder = await folderRepository.GetByIdAsync(folderId);
       if (folder == null)
           return ApiResponse<Guid>.ErrorResponse($"{folderId} folder no encontrado");
       
       await folderRepository.DeleteAsync(folder);
       
       return ApiResponse<Guid>.SuccessResponse(folder.FolderId);
    }

    public async Task<ApiResponse<IEnumerable<FolderDTos>>> ListarFoldersAsync()
    {
        var folderList = await folderRepository.GetAll();

        IEnumerable<FolderDTos> folderDTosEnumerable = folderList.Select(x => new FolderDTos(FolderId: x.FolderId, Name: x.Name));
    
        return ApiResponse<IEnumerable<FolderDTos>>.SuccessResponse(folderDTosEnumerable);
    }

    public async Task<ApiResponse<FolderDTos>> ObtenerFolderId(Guid folderId)
    {
        var folder = await folderRepository.GetByIdAsync(folderId);
        if (folder == null)
            return ApiResponse<FolderDTos>.ErrorResponse($"{folderId} folder no encontrado");
        
        FolderDTos folderDTos = new(FolderId: folder.FolderId, Name: folder.Name);
        
        return ApiResponse<FolderDTos>.SuccessResponse(folderDTos);
    }
}