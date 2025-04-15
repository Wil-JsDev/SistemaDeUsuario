using Microsoft.EntityFrameworkCore;
using SistemaDeUsuarios.Context;
using SistemaDeUsuarios.Interfaces.Repository;

namespace SistemaDeUsuarios.Repository;

public class GenericRepository<TEntity>(SistemaDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext _context = context;

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await SaveAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await SaveAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        await SaveAsync();
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAll() =>
        await _context.Set<TEntity>().ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        return entity;
    }
    public virtual async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}