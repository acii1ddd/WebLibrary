using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class AuthorRepository(LibraryContext context) : IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken ct)
    {
        return await context.Authors
            .AsNoTracking()
            .Include(x => x.Books)
            .ToListAsync(ct);
    }

    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Authors
            .FirstOrDefaultAsync(x => x.Id == id, ct);
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