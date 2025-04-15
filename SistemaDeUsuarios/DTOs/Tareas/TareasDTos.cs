using SistemaDeUsuarios.Enum;

namespace SistemaDeUsuarios.DTOs.Tareas;

public record TareasDTos
(
    Guid TareaId,
    string? Titulo,
    string? Contenido,
    Guid FolderId,
    Prioridad? PrioridadDeTarea
);