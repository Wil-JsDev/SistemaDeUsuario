using Microsoft.AspNetCore.Identity;
using SistemaDeUsuarios.Enum;
using SistemaDeUsuarios.Models;

namespace SistemaDeUsuarios.Seeds;

public static class RolesPorDefecto
{
    public static async Task SeedAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Estudiante.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Maestro.ToString()));
    }
}