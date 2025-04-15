using SistemaDeUsuarios.Enum;

namespace SistemaDeUsuarios.DTOs.Tareas;

public record CrearTareasDTos
(
    string? Titulo,
    string? Contenido,
    Guid FolderId,
    Prioridad PrioridadDeTarea
);