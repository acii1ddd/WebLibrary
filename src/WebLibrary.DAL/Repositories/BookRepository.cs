using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class BookRepository(LibraryContext context): IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct)
    {
        return await context.Books
            .AsNoTracking()
            .Include(x => x.Author)
            .ToListAsync(ct);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Books
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(Book book, CancellationToken ct)
    {
        await context.Books.AddAsync(book, ct);
        
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Book book, CancellationToken ct)
    {
        context.Books.Update(book);

        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Book book, CancellationToken ct)
    {
        context.Books.Remove(book);

        await context.SaveChangesAsync(ct);
    }
}