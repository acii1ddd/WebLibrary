using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.API.Contracts.Contracts.Authors.Responses;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<GetAuthorResponse>> GetAllAsync();

    public Task<GetAuthorResponse> GetByIdAsync(Guid id);
    
    public Task<Guid> AddAsync(AddAuthorRequest author);
    
    public Task UpdateAsync(UpdateAuthorRequest updateAuthorRequest, Guid id);
    
    public Task DeleteAsync(Guid id);

    public Task<IEnumerable<GetAuthorsWithBooksCountResponse>> GetAuthorsWithBookCountsAsync();
}