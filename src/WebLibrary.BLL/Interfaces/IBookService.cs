using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<Book>> GetAllAsync();

    public Task<Book> GetByIdAsync(Guid id);
    
    public Task<Guid> AddAsync(AddBookRequest author);
    
    public Task UpdateAsync(UpdateBookRequest author, Guid id);
    
    public Task DeleteAsync(Guid id);
}