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
    public async Task<IEnumerable<GetBookResponse>> GetAllAsync(int? startYear)
    {
        var books = await bookRepository.GetAllAsync();
        
        if (startYear is not null)
        {
            books = books.Where(x => x.PublishedYear > startYear);
        }
        
        return books.Adapt<IEnumerable<GetBookResponse>>();
    }

    public async Task<GetBookResponse> GetByIdAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        return book.Adapt<GetBookResponse>();  
    }

    public async Task<Guid> AddAsync(AddBookRequest addBookRequest)
    {
        addBookRequest.Validate();
        
        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = addBookRequest.Title,
            PublishedYear = addBookRequest.PublishedYear
        };
        
        await bookRepository.AddAsync(newBook);
        
        return newBook.Id;
    }

    public async Task UpdateAsync(UpdateBookRequest updateBookRequest, Guid id)
    {
        updateBookRequest.Validate();
        
        var bookToUpdate = await bookRepository.GetByIdAsync(id);

        if (bookToUpdate is null)
            throw new NotFoundException("Book", id);

        bookToUpdate.Title = updateBookRequest.Title;
        bookToUpdate.PublishedYear = updateBookRequest.PublishedYear;
        
        await bookRepository.UpdateAsync(bookToUpdate);
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        await bookRepository.DeleteByIdAsync(id);
    }
}