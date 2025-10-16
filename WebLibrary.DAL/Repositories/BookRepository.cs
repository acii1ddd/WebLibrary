using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _items = [];
    private readonly Lock _lock = new Lock();

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(_items.AsEnumerable());
        }
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);    
        }
    }

    public Task AddAsync(Book entity)
    {
        lock (_lock)
        {
            _items.Add(entity);
            return Task.CompletedTask;
        }
    }

    public Task UpdateAsync(Book entity)
    {
        lock (_lock)
        {
            var itemIndex = _items.FindIndex(x => x.Id == entity.Id);
            if (itemIndex >= 0)
                _items[itemIndex] = entity;
        
            return Task.CompletedTask;   
        }
    }

    public Task DeleteByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var itemIndex = _items.FindIndex(x => x.Id == id);
            if (itemIndex >= 0)
                _items.RemoveAt(itemIndex);
        
            return Task.CompletedTask;   
        }
    }
}