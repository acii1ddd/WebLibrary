namespace WebLibrary.BLL.Interfaces;

public interface IService<T>
{
    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T?> GetByIdAsync(Guid id);
    
    public Task AddAsync(T author);
    
    public Task UpdateAsync(T author);
    
    public Task DeleteAsync(Guid id);
}