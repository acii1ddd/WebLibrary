using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.BLL.Exceptions;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    public Task<IEnumerable<Author>> GetAllAsync()
    {
        return authorRepository.GetAllAsync();
    }

    public async Task<Author> GetByIdAsync(Guid id)
    {
        var author = await authorRepository.GetByIdAsync(id);

        if (author == null)
            throw new NotFoundException("Author", id);
        
        return author;
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
        
        var updatedAuthor = new Author
        {
            Id = id,
            Name = updateAuthorRequest.Name,
            DateOfBirth = updateAuthorRequest.DateOfBirth
        };
        
        await authorRepository.UpdateAsync(updatedAuthor);
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await authorRepository.GetByIdAsync(id);

        if (author is null)
            throw new NotFoundException("Author", id);
        
        await authorRepository.DeleteByIdAsync(id);
    }
}