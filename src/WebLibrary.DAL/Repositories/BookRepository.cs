using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private static readonly List<Book> Items = [];
    private readonly Lock _lock = new();

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(Items.AsEnumerable());
        }
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);    
        }
    }

    public Task AddAsync(Book book)
    {
        lock (_lock)
        {
            Items.Add(book);
            return Task.CompletedTask;
        }
    }

    public Task UpdateAsync(Book book)
    {
        lock (_lock)
        {
            var itemIndex = Items.FindIndex(x => x.Id == book.Id);
            if (itemIndex >= 0)
                Items[itemIndex] = book;
        
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