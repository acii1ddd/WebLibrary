using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.API.Contracts.Contracts.Books.Responses;

namespace WebLibrary.BLL.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<GetBookResponse>> GetAllAsync(int? startYear, CancellationToken ct);

    public Task<GetBookResponse> GetByIdAsync(Guid id, CancellationToken ct);
    
    public Task<Guid> AddAsync(AddBookRequest author, CancellationToken ct);
    
    public Task UpdateAsync(UpdateBookRequest author, Guid id, CancellationToken ct);
    
    public Task DeleteAsync(Guid id, CancellationToken ct);
}