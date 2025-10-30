using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class BookRepository(LibraryContext context): IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync(int? startYear, CancellationToken ct)
    {
        var query = context.Books
            .AsNoTracking()
            .Include(x => x.Authors)
            .AsQueryable();

        if (startYear.HasValue)
        {
            query = query.Where(x => x.PublishedYear > startYear);
        }
        
        return await query.ToListAsync(ct);
    }

    /// <summary>
    /// Получение книги по индентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги для поиска</param>
    /// <param name="ct">Токен отмены операции</param>
    /// <param name="track">false - получаем сущность без отслеживания;
    /// true - получаем сущность с отслеживанием;
    /// значение по умолчанию - false</param>
    /// <returns>Объект книги или null в случае не нахождения</returns>
    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct, bool track = false)
    {
        var query = context.Books.AsQueryable();

        if (!track)
        {
            query = query.AsNoTracking();
        }
        
        return await query
            .Include(x => x.Authors)
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