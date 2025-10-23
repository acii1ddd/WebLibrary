using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private static readonly List<Author> Items = [];
    private readonly Lock _lock = new();

    public Task<IEnumerable<Author>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(Items.AsEnumerable());
        }
    }

    public Task<Author?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);    
        }
    }

    public Task AddAsync(Author author)
    {
        lock (_lock)
        {
            Items.Add(author);
            return Task.CompletedTask;
        }
    }

    public Task UpdateAsync(Author author)
    {
        lock (_lock)
        {
            var itemIndex = Items.FindIndex(x => x.Id == author.Id);
            if (itemIndex >= 0)
                Items[itemIndex] = author;
        
            return Task.CompletedTask;   
        }
    }

    public Task DeleteByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var itemIndex = Items.FindIndex(x => x.Id == id);
            if (itemIndex >= 0)
                Items.RemoveAt(itemIndex);
        
            return Task.CompletedTask;   
        }
    }
}