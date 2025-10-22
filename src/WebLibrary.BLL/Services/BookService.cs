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

    public async Task<Guid> AddAsync(AddBookRequest book)
    {
        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = book.Title,
            PublishedYear = book.PublishedYear
        };
        
        await bookRepository.AddAsync(newBook);
        
        return newBook.Id;
    }

    public async Task UpdateAsync(UpdateBookRequest updateBookRequest, Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException("Book", id);
        
        var updatedBook = new Book
        {
            Id = id,
            Title = book.Title,
            PublishedYear = book.PublishedYear
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