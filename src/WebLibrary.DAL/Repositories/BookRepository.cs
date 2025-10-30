using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class BookRepository(LibraryContext context): IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await context.Books
            .AsNoTracking()
            .Include(x => x.Author)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await context.Books
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Book book)
    {
        await context.Books.AddAsync(book);
        
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        context.Books.Update(book);

        await context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var book = context.Books
            .FirstOrDefault(x => x.Id == id);
        
        context.Books.Remove(book!);

        await context.SaveChangesAsync();
    }
}