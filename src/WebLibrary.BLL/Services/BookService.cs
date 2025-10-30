using Mapster;
using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.API.Contracts.Contracts.Books.Responses;
using WebLibrary.BLL.Exceptions;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class BookService(IBookRepository bookRepository) : IBookService
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
        addBookRequest.Validate();
        
        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = addBookRequest.Title,
            PublishedYear = addBookRequest.PublishedYear,
            AuthorId = addBookRequest.AuthorId
        };
        
        await bookRepository.AddAsync(newBook, ct);
        
        return newBook.Id;
    }

    public async Task UpdateAsync(UpdateBookRequest updateBookRequest, Guid id, 
        CancellationToken ct)
    {
        updateBookRequest.Validate();
        
        var bookToUpdate = await bookRepository.GetByIdAsync(id, ct);

        if (bookToUpdate is null)
            throw new NotFoundException("Book", id);

        bookToUpdate.Title = updateBookRequest.Title;
        bookToUpdate.PublishedYear = updateBookRequest.PublishedYear;
        bookToUpdate.AuthorId = updateBookRequest.AuthorId;
        
        await bookRepository.UpdateAsync(bookToUpdate, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var book = await bookRepository.GetByIdAsync(id, ct);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        await bookRepository.DeleteByIdAsync(id, ct);
    }
}