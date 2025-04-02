using Microsoft.AspNetCore.Identity;

namespace SistemaDeUsuarios.Models;

public class Users : IdentityUser
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set;}
    
    public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
}