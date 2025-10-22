using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return bookRepository.GetAllAsync();
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        return bookRepository.GetByIdAsync(id);      
    }

    public Task AddAsync(AddBookRequest author)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateBookRequest author)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Book book)
    {
        return bookRepository.AddAsync(book);
    }

    public Task UpdateAsync(Book book)
    {
        return bookRepository.UpdateAsync(book);
    }

    public Task DeleteAsync(Guid id)
    {
        return bookRepository.DeleteByIdAsync(id);
    }
}