using Microsoft.EntityFrameworkCore;
using SistemaDeUsuarios.Context;
using SistemaDeUsuarios.Interfaces.Repository;
using SistemaDeUsuarios.Models;

namespace SistemaDeUsuarios.Repository;

public class FolderRepository(SistemaDbContext context) : GenericRepository<Folder>(context), IFolderRepository
{
}
