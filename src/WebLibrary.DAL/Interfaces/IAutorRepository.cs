using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    public Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken ct);

    public Task<(IEnumerable<Author> Items, int TotalCount)> GetAllAsync(
        string? name, int pageNumber, int pageSize, CancellationToken ct);
}
