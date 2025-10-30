namespace WebLibrary.DAL.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(Guid id, CancellationToken ct, bool track = false);
    
    public Task AddAsync(T entity, CancellationToken ct);
    
    public Task UpdateAsync(T entity, CancellationToken ct);
    
    public Task DeleteAsync(T entity, CancellationToken ct);
}