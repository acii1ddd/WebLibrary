using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    
    public Task<T?> GetByIdAsync(Guid id);
    
    public Task AddAsync(T entity);
    
    public Task UpdateAsync(T entity);
    
    public Task DeleteByIdAsync(Guid id);
}