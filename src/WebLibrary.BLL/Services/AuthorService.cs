using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.API.Contracts.Contracts.Authors.Responses;
using WebLibrary.BLL.Exceptions;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;
using Mapster;
using WebLibrary.API.Contracts.Contracts;

namespace WebLibrary.BLL.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    public async Task<PagedResult<GetAuthorResponse>> GetAllAsync(
        PagedQueryParams @params, GetAuthorQueryFilters filters, CancellationToken ct)
    {
        @params.ValidateAndThrow();
        
        var (items, totalCount) = await authorRepository
            .GetAllAsync(filters.Name, @params.PageNumber, @params.PageSize, ct);

        var pagedAuthors = new PagedResult<GetAuthorResponse>
        {
            Items = items.Adapt<IEnumerable<GetAuthorResponse>>(),
            TotalCount = totalCount,
            PageNumber = @params.PageNumber,
            PageSize = @params.PageSize
        };

        return pagedAuthors;
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
        addAuthorRequest.ValidateAndThrow();
        
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
        updateAuthorRequest.ValidateAndThrow();
        
        var authorToUpdate = await authorRepository.GetByIdAsync(id, ct, true);

        if (authorToUpdate is null)
            throw new NotFoundException("Author", id);

        authorToUpdate.Name = updateAuthorRequest.Name;
        authorToUpdate.DateOfBirth = updateAuthorRequest.DateOfBirth;
    
        await authorRepository.UpdateAsync(authorToUpdate, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(id, ct, true);

        if (author is null)
            throw new NotFoundException("Author", id);
        
        await authorRepository.DeleteAsync(author, ct);
    }
    
    public async Task<PagedResult<GetAuthorsWithBooksCountResponse>>
        GetAuthorsWithBookCountsAsync(PagedQueryParams @params, CancellationToken ct)
    {
        @params.ValidateAndThrow();
        
        var (authors, totalCount) = await authorRepository
            .GetAllAsync(null, @params.PageNumber,@params.PageSize, ct);

        var authorsWithBookCount = authors
            .Select(x => new GetAuthorsWithBooksCountResponse
        {
            Id = x.Id,
            Name = x.Name,
            BookCount = x.Books.Count
        });

        var pagedAuthors = new PagedResult<GetAuthorsWithBooksCountResponse>
        {
            Items = authorsWithBookCount,
            TotalCount = totalCount,
            PageNumber = @params.PageNumber,
            PageSize = @params.PageSize
        };
        
        return pagedAuthors;
    }
}