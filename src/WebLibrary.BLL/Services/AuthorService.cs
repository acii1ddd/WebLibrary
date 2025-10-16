using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    public Task<IEnumerable<Author>> GetAllAsync()
    {
        return authorRepository.GetAllAsync();
    }

    public Task<Author?> GetByIdAsync(Guid id)
    {
        return authorRepository.GetByIdAsync(id);
    }

    public Task AddAsync(Author author)
    {
        return authorRepository.AddAsync(author);
    }

    public Task UpdateAsync(Author author)
    {
        return authorRepository.UpdateAsync(author);
    }

    public Task DeleteAsync(Guid id)
    {
        return authorRepository.DeleteByIdAsync(id);
    }
}