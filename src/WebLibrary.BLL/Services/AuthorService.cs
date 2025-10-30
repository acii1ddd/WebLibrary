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
    public async Task<IEnumerable<GetAuthorResponse>> GetAllAsync(string? name)
    {
        var authors = await authorRepository.GetAllAsync();

        if (name is not null)
        {
            authors = authors.Where(x => 
                x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
        }
        
        return authors.Adapt<IEnumerable<GetAuthorResponse>>();
    }

    public async Task<GetAuthorResponse> GetByIdAsync(Guid id)
    {
        var author = await authorRepository.GetByIdAsync(id);

        if (author == null)
            throw new NotFoundException("Author", id);
        
        return author.Adapt<GetAuthorResponse>();
    }

    public async Task<Guid> AddAsync(AddAuthorRequest addAuthorRequest)
    {
        addAuthorRequest.Validate();
        
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = addAuthorRequest.Name,
            DateOfBirth = addAuthorRequest.DateOfBirth
        };
        
        await authorRepository.AddAsync(author);
        
        return author.Id;
    }

    public async Task UpdateAsync(UpdateAuthorRequest updateAuthorRequest, Guid id)
    {
        updateAuthorRequest.Validate();
        
        var authorToUpdate = await authorRepository.GetByIdAsync(id);

        if (authorToUpdate is null)
            throw new NotFoundException("Author", id);

        authorToUpdate.Name = updateAuthorRequest.Name;
        authorToUpdate.DateOfBirth = updateAuthorRequest.DateOfBirth;
    
        await authorRepository.UpdateAsync(authorToUpdate);
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await authorRepository.GetByIdAsync(id);

        if (author is null)
            throw new NotFoundException("Author", id);
        
        await authorRepository.DeleteByIdAsync(id);
    }
    
    public async Task<IEnumerable<GetAuthorsWithBooksCountResponse>> GetAuthorsWithBookCountsAsync()
    {
        var authors = await authorRepository.GetAllAsync();

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