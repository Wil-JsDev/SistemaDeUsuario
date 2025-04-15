using SistemaDeUsuarios.Enum;

namespace SistemaDeUsuarios.DTOs.Tareas;

public record ActualizarTareasDTos
(
    string? Titulo,
    string? Contenido,
    Prioridad PrioridadDeTarea
);