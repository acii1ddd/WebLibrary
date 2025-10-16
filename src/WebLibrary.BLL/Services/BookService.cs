using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class BookService(IBookRepository bookRepository) : IService<Book>
{
    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return bookRepository.GetAllAsync();
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        return bookRepository.GetByIdAsync(id);      
    }

    public Task AddAsync(Book entity)
    {
        return bookRepository.AddAsync(entity);
    }

    public Task UpdateAsync(Book entity)
    {
        return bookRepository.UpdateAsync(entity);
    }

    public Task DeleteAsync(Book entity)
    {
        return bookRepository.DeleteByIdAsync(entity.Id);
    }
}