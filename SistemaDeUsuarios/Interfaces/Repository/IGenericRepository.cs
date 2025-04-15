namespace SistemaDeUsuarios.Interfaces.Repository;

public interface IGenericRepository<TEntity> 
{
    Task CreateAsync(TEntity entity);
    
    Task UpdateAsync(TEntity entity);
    
    Task DeleteAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAll();
    
    Task<TEntity?> GetByIdAsync(Guid id);
    
    Task SaveAsync();
}