using WebLibrary.API.Contracts.Authors.Requests;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<Author>> GetAllAsync();

    public Task<Author> GetByIdAsync(Guid id);
    
    public Task<Guid> AddAsync(AddAuthorRequest author);
    
    public Task UpdateAsync(UpdateAuthorRequest updateAuthorRequest, Guid id);
    
    public Task DeleteAsync(Guid id);
}