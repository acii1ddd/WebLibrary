using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.API.Contracts.Contracts.Authors.Responses;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<GetAuthorResponse>> GetAllAsync(string? name, CancellationToken ct);

    public Task<GetAuthorResponse> GetByIdAsync(Guid id, CancellationToken ct);
    
    public Task<Guid> AddAsync(AddAuthorRequest author, CancellationToken ct);
    
    public Task UpdateAsync(UpdateAuthorRequest updateAuthorRequest, Guid id, CancellationToken ct);
    
    public Task DeleteAsync(Guid id, CancellationToken ct);

    public Task<IEnumerable<GetAuthorsWithBooksCountResponse>> 
        GetAuthorsWithBookCountsAsync(CancellationToken ct);
}