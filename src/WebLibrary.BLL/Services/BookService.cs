using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.BLL.Exceptions;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Models;

namespace WebLibrary.BLL.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await bookRepository.GetAllAsync();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        return book;  
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
        
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        var updatedBook = new Book
        {
            Id = id,
            Title = updateBookRequest.Title,
            PublishedYear = updateBookRequest.PublishedYear
        };
        
        await bookRepository.UpdateAsync(updatedBook);
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        await bookRepository.DeleteByIdAsync(id);
    }
}