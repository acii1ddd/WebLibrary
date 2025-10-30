using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    public Task<(IEnumerable<Book> Items, int TotalCount)> GetAllAsync(int? startYear,
        int pageNumber, int pageSize, CancellationToken ct);
}