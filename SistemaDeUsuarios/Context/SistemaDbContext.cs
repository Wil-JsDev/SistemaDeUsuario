using Microsoft.EntityFrameworkCore;
using SistemaDeUsuarios.Enum;
using SistemaDeUsuarios.Models;

namespace SistemaDeUsuarios.Context;

public class SistemaDbContext : DbContext
{
    public SistemaDbContext(DbContextOptions<SistemaDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Folder> Folders { get; set; }
    
    public DbSet<Tareas> Tareas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Pk

        modelBuilder.Entity<Folder>()
            .HasKey(x => x.FolderId);

        modelBuilder.Entity<Tareas>()
            .HasKey(x => x.NotaId);
        #endregion

        #region Relaciones
        modelBuilder.Entity<Tareas>()
            .HasOne(x => x.Folder)
            .WithMany(x => x.Tareas)
            .HasForeignKey(x => x.FolderId)
            .HasConstraintName("FK_FolderId")
            .IsRequired();
        #endregion
        
        #region Folder
        
        modelBuilder.Entity<Folder>()
            .Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        #endregion
        
        #region Notas
        
        modelBuilder.HasPostgresEnum<Prioridad>();

        modelBuilder.Entity<Tareas>()
            .Property(x => x.Titulo)
            .HasMaxLength(50)
            .IsRequired();
        
        modelBuilder.Entity<Tareas>()
            .Property(x => x.Contenido)
            .HasMaxLength(200)
            .IsRequired();
        
        #endregion
    }
}
