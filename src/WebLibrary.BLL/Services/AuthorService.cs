using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class AuthorService(IAuthorRepository authorRepository) : IService<Author>
{
    public Task<IEnumerable<Author>> GetAllAsync()
    {
        return authorRepository.GetAllAsync();
    }

    public Task<Author?> GetByIdAsync(Guid id)
    {
        return authorRepository.GetByIdAsync(id);
    }

    public Task AddAsync(Author entity)
    {
        return authorRepository.AddAsync(entity);
    }

    public Task UpdateAsync(Author entity)
    {
        return authorRepository.UpdateAsync(entity);
    }

    public Task DeleteAsync(Author entity)
    {
        return authorRepository.DeleteByIdAsync(entity.Id);
    }
}