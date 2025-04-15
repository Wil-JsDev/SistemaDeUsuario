using SistemaDeUsuarios.DTOs.Folder;
using SistemaDeUsuarios.DTOs.Tareas;
using SistemaDeUsuarios.Enum;
using SistemaDeUsuarios.Interfaces;
using SistemaDeUsuarios.Interfaces.Repository;
using SistemaDeUsuarios.Models;
using SistemaDeUsuarios.Utils;

namespace SistemaDeUsuarios.Services;

public class TareasServicios (ITareasRepository tareasRepository) : ITareasServicios
{
    public async Task<ApiResponse<TareasDTos>> CrearTareasAsync(CrearTareasDTos crearTareasDTos)
    {
        if (crearTareasDTos == null)
            return ApiResponse<TareasDTos>.ErrorResponse("La informacion no puede ser nula");


        Tareas tareas = new()
        {
            NotaId = Guid.NewGuid(),
            Titulo = crearTareasDTos.Titulo,
            Contenido = crearTareasDTos.Contenido,
            FolderId = crearTareasDTos.FolderId,
            PrioridadDeTarea = crearTareasDTos.PrioridadDeTarea
        };

        await tareasRepository.CreateAsync(tareas);

        TareasDTos tareasDTos = new
        (
            TareaId: tareas.NotaId,
            Titulo: tareas.Titulo,
            Contenido: tareas.Contenido,
            FolderId: tareas.FolderId,
            PrioridadDeTarea: tareas.PrioridadDeTarea
        );
        
        return ApiResponse<TareasDTos>.SuccessResponse(tareasDTos);
    }

    public async Task<ApiResponse<TareasDTos>> ActualizarTareasAsync(Guid tareasId,ActualizarTareasDTos actualizarTareasDTos)
    {
       var tareas = await tareasRepository.GetByIdAsync(tareasId);
       if (tareas == null)
           return ApiResponse<TareasDTos>.ErrorResponse($"{tareasId} no encontrado");
       
       tareas.Titulo = actualizarTareasDTos.Titulo;
       tareas.Contenido = actualizarTareasDTos.Contenido;
       tareas.PrioridadDeTarea = actualizarTareasDTos.PrioridadDeTarea;

       await tareasRepository.UpdateAsync(tareas);
        
       TareasDTos tareasDTos = new
       (
           TareaId: tareas.NotaId,
           Titulo: tareas.Titulo,
           Contenido: tareas.Contenido,
           FolderId: tareas.FolderId,
           PrioridadDeTarea: tareas.PrioridadDeTarea
       );
       
       return ApiResponse<TareasDTos>.SuccessResponse(tareasDTos);
    }

    public async Task<ApiResponse<Guid>> BorrarTareasAsync(Guid tareasId)
    {
        var tareas = await tareasRepository.GetByIdAsync(tareasId);
        if (tareas == null)
            return ApiResponse<Guid>.ErrorResponse($"{tareasId} no encontrado");
        
        await tareasRepository.DeleteAsync(tareas);
        
        return ApiResponse<Guid>.SuccessResponse(tareas.NotaId);
    }

    public async Task<ApiResponse<IEnumerable<TareasDTos>>> ListarTareasAsync()
    {
        var tareasList = await tareasRepository.GetAll();

        IEnumerable<TareasDTos> tareasDTosEnumerable = tareasList.Select(x => new TareasDTos
        (
            TareaId: x.NotaId,
            Titulo: x.Titulo,
            Contenido: x.Contenido,
            FolderId: x.FolderId,
            PrioridadDeTarea: x.PrioridadDeTarea
        ));
        
        return ApiResponse<IEnumerable<TareasDTos>>.SuccessResponse(tareasDTosEnumerable);
    }

    public async Task<ApiResponse<TareasDTos>> ObtenerTareasId(Guid tareasId)
    {
        var tareas = await tareasRepository.GetByIdAsync(tareasId);
        if (tareas == null)
            return ApiResponse<TareasDTos>.ErrorResponse($"{tareasId} no encontrado");
        
        TareasDTos tareasDTos = new
        (
            TareaId: tareas.NotaId,
            Titulo: tareas.Titulo,
            Contenido: tareas.Contenido,
            FolderId: tareas.FolderId,
            PrioridadDeTarea: tareas.PrioridadDeTarea
        );
        
        return ApiResponse<TareasDTos>.SuccessResponse(tareasDTos);
    }
}