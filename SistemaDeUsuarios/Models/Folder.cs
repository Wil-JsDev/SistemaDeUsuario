namespace SistemaDeUsuarios.Models;

public sealed class Folder
{
    public Guid FolderId { get; set; }
    
    public string? Name { get; set; }
    public ICollection<Tareas> Tareas { get; set; }
}