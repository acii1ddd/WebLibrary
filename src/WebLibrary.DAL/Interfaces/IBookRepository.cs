using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    public Task<IEnumerable<Book>> GetAllAsync(int? startYear, CancellationToken ct);
}