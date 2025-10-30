using WebLibrary.API.Contracts.Contracts;
using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.API.Contracts.Contracts.Authors.Responses;

namespace WebLibrary.BLL.Interfaces;

public interface IAuthorService
{
    public Task<PagedResult<GetAuthorResponse>> GetAllAsync(
        PagedQueryParams @params, GetAuthorQueryFilters filters, CancellationToken ct
    );

    public Task<GetAuthorResponse> GetByIdAsync(Guid id, CancellationToken ct);
    
    public Task<Guid> AddAsync(AddAuthorRequest author, CancellationToken ct);
    
    public Task UpdateAsync(UpdateAuthorRequest updateAuthorRequest, Guid id, CancellationToken ct);
    
    public Task DeleteAsync(Guid id, CancellationToken ct);

    public Task<PagedResult<GetAuthorsWithBooksCountResponse>>
        GetAuthorsWithBookCountsAsync(PagedQueryParams @params, CancellationToken ct);
}