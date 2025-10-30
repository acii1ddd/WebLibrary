using System.ComponentModel.DataAnnotations;
using Mapster;
using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.API.Contracts.Contracts.Books.Responses;
using WebLibrary.BLL.Exceptions;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class BookService(
    IBookRepository bookRepository, 
    IAuthorRepository authorRepository) : IBookService
{
    public async Task<IEnumerable<GetBookResponse>> GetAllAsync(int? startYear, CancellationToken ct)
    {
        var books = await bookRepository.GetAllAsync(ct);
        
        if (startYear is not null)
        {
            books = books.Where(x => x.PublishedYear > startYear);
        }
        
        return books.Adapt<IEnumerable<GetBookResponse>>();
    }

    public async Task<GetBookResponse> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var book = await bookRepository.GetByIdAsync(id, ct);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        return book.Adapt<GetBookResponse>();  
    }

    public async Task<Guid> AddAsync(AddBookRequest addBookRequest, CancellationToken ct)
    {
        addBookRequest.ValidateAndThrow();

        await ValidateAuthorsIdsAndThrowAsync(addBookRequest.Authors, ct);

        var authors = await authorRepository
            .GetByIdsAsync(addBookRequest.Authors, ct);
        
        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = addBookRequest.Title,
            PublishedYear = addBookRequest.PublishedYear,
            Authors = authors
        };
        
        await bookRepository.AddAsync(newBook, ct);
        
        return newBook.Id;
    }

    private async Task ValidateAuthorsIdsAndThrowAsync(List<Guid> authorsIds, CancellationToken ct)
    {
        var authors = await authorRepository.GetByIdsAsync(authorsIds, ct);

        var foundIds = authors
            .Select(x => x.Id)
            .ToHashSet();
        
        var missingIds = authorsIds
            .Where(x => !foundIds.Contains(x))
            .ToList();
        
        if (missingIds.Count != 0)
        {
            throw new ValidationException($"Authors with ids \'{string.Join(",", missingIds)}\' " +
                                          $"was not found");
        }
    }

    public async Task UpdateAsync(UpdateBookRequest updateBookRequest, Guid id, 
        CancellationToken ct)
    {
        updateBookRequest.ValidateAndThrow();
        
        await ValidateAuthorsIdsAndThrowAsync(updateBookRequest.Authors, ct);
        
        var bookToUpdate = await bookRepository.GetByIdAsync(id, ct);

        if (bookToUpdate is null)
            throw new NotFoundException("Book", id);

        var newAuthors = await authorRepository
            .GetByIdsAsync(updateBookRequest.Authors, ct);

        bookToUpdate.Authors = newAuthors;
        bookToUpdate.Title = updateBookRequest.Title;
        bookToUpdate.PublishedYear = updateBookRequest.PublishedYear;
        
        await bookRepository.UpdateAsync(bookToUpdate, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var book = await bookRepository.GetByIdAsync(id, ct);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        await bookRepository.DeleteAsync(book, ct);
    }
}