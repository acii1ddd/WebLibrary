using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class AuthorRepository(LibraryContext context) : IAuthorRepository
{
    public async Task<(IEnumerable<Author> Items, int TotalCount)> GetAllAsync(int pageNumber, 
        int pageSize, string? name, CancellationToken ct)
    {
        var query = context.Authors.AsQueryable();

        if (name is not null)
        {
            query = query.Where(
                x => x.Name.ToLower().Contains(name.ToLower())
            );
        }
        
        var totalCount = await query.CountAsync(ct);
        
        query = query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        return (await query.ToListAsync(ct), totalCount);
    }

    /// <summary>
    /// Получение автора по индентификатору
    /// </summary>
    /// <param name="id">Идентификатор автора для поиска</param>
    /// <param name="ct">Токен отмены операции</param>
    /// <param name="track">false - получаем сущность без отслеживания;
    /// true - получаем сущность с отслеживанием;
    /// значение по умолчанию - false</param>
    /// <returns>Объект автора или null в случае не нахождения</returns>
    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken ct, bool track = false)
    {
        var query = context.Authors.AsQueryable();

        if (!false)
        {
            query = query.AsNoTracking();
        }
     
        return await query.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(Author author, CancellationToken ct)
    {
        await context.Authors.AddAsync(author, ct);
        
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Author author, CancellationToken ct)
    {
        context.Authors.Update(author);

        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Author author, CancellationToken ct)
    {
        context.Authors.Remove(author);

        await context.SaveChangesAsync(ct);
    }

    public async Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken ct)
    {
        return await context.Authors
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(ct);
    }
}