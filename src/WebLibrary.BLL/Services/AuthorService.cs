using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.API.Contracts.Contracts.Authors.Responses;
using WebLibrary.BLL.Exceptions;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;
using Mapster;

namespace WebLibrary.BLL.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    public async Task<IEnumerable<GetAuthorResponse>> GetAllAsync(string? name, CancellationToken ct)
    {
        var authors = await authorRepository.GetAllAsync(ct);

        if (name is not null)
        {
            authors = authors.Where(x => 
                x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
        }
        
        return authors.Adapt<IEnumerable<GetAuthorResponse>>();
    }

    public async Task<GetAuthorResponse> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(id, ct);

        if (author == null)
            throw new NotFoundException("Author", id);
        
        return author.Adapt<GetAuthorResponse>();
    }

    public async Task<Guid> AddAsync(AddAuthorRequest addAuthorRequest, CancellationToken ct)
    {
        addAuthorRequest.Validate();
        
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = addAuthorRequest.Name,
            DateOfBirth = addAuthorRequest.DateOfBirth
        };
        
        await authorRepository.AddAsync(author, ct);
        
        return author.Id;
    }

    public async Task UpdateAsync(UpdateAuthorRequest updateAuthorRequest, Guid id, 
        CancellationToken ct)
    {
        updateAuthorRequest.Validate();
        
        var authorToUpdate = await authorRepository.GetByIdAsync(id, ct);

        if (authorToUpdate is null)
            throw new NotFoundException("Author", id);

        authorToUpdate.Name = updateAuthorRequest.Name;
        authorToUpdate.DateOfBirth = updateAuthorRequest.DateOfBirth;
    
        await authorRepository.UpdateAsync(authorToUpdate, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(id, ct);

        if (author is null)
            throw new NotFoundException("Author", id);
        
        await authorRepository.DeleteByIdAsync(id, ct);
    }
    
    public async Task<IEnumerable<GetAuthorsWithBooksCountResponse>> 
        GetAuthorsWithBookCountsAsync(CancellationToken ct)
    {
        var authors = await authorRepository.GetAllAsync(ct);

        var authorsWithBookCount = authors
            .Select(x => new GetAuthorsWithBooksCountResponse
        {
            Id = x.Id,
            Name = x.Name,
            BookCount = x.Books.Count
        });

        return authorsWithBookCount;
    }
}