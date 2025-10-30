using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    public Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken ct);

    public Task<(IEnumerable<Author> Items, int TotalCount)> GetAllAsync(int pageNumber,
        int pageSize, string? name, CancellationToken ct);
}
