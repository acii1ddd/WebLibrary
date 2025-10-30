using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    public Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken ct);
    
    public Task<IEnumerable<Author>> GetAllAsync(string? name, CancellationToken ct);
}
