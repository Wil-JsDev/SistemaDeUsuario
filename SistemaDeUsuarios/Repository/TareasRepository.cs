using Microsoft.EntityFrameworkCore;
using SistemaDeUsuarios.Context;
using SistemaDeUsuarios.Interfaces.Repository;
using SistemaDeUsuarios.Models;

namespace SistemaDeUsuarios.Repository;

public class TareasRepository(SistemaDbContext context) : GenericRepository<Tareas>(context), ITareasRepository
{
    
}