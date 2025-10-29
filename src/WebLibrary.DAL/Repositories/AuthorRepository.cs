using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Repositories;

public class AuthorRepository(LibraryContext context) : IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await context.Authors
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await context.Authors
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Author author)
    {
        await context.Authors.AddAsync(author);
        
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        context.Authors.Update(author);

        await context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var author = context.Authors
            .FirstOrDefault(x => x.Id == id);
        
        context.Authors.Remove(author!);

        await context.SaveChangesAsync();
    }
}