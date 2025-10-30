using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.BLL.Interfaces;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int? startYear)
    {
        var books = await bookService.GetAllAsync(startYear);
        
        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var author = await bookService.GetByIdAsync(id);
        
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddBookRequest addBookRequest)
    {
        var bookId = await bookService.AddAsync(addBookRequest);
        
        return Created($"/api/books/{bookId}", new {id = bookId});
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateBookRequest updateBookRequest,
        [FromRoute] Guid id)
    {
        await bookService.UpdateAsync(updateBookRequest, id);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await bookService.DeleteAsync(id);

        return NoContent();
    }
}