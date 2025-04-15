using SistemaDeUsuarios.Enum;

namespace SistemaDeUsuarios.Models;

public sealed class Tareas
{
    public Guid NotaId { get; set; }
    
    public string? Titulo {  get; set; }

    public string? Contenido { get; set; }

    public Guid FolderId { get; set; }
    
    public Folder Folder { get; set; }
    
    public Prioridad PrioridadDeTarea { get; set; }

    public DateTime? FechaDeCreacion { get; set; } = DateTime.UtcNow;
}