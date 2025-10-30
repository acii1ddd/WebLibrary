using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.API.Contracts.Contracts.Books.Responses;

namespace WebLibrary.BLL.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<GetBookResponse>> GetAllAsync(int? startYear);

    public Task<GetBookResponse> GetByIdAsync(Guid id);
    
    public Task<Guid> AddAsync(AddBookRequest author);
    
    public Task UpdateAsync(UpdateBookRequest author, Guid id);
    
    public Task DeleteAsync(Guid id);
}