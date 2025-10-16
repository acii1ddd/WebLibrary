using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly List<Author> _items = [];
    private readonly Lock _lock = new();

    public Task<IEnumerable<Author>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(_items.AsEnumerable());
        }
    }

    public Task<Author?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);    
        }
    }

    public Task AddAsync(Author author)
    {
        lock (_lock)
        {
            _items.Add(author);
            return Task.CompletedTask;
        }
    }

    public Task UpdateAsync(Author author)
    {
        lock (_lock)
        {
            var itemIndex = _items.FindIndex(x => x.Id == author.Id);
            if (itemIndex >= 0)
                _items[itemIndex] = author;
        
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