namespace WebLibrary.BLL.Interfaces;

public interface IService<T>
{
    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T?> GetByIdAsync(Guid id);
    
    public Task AddAsync(T entity);
    
    public Task UpdateAsync(T entity);
    
    public Task DeleteAsync(T entity);
}