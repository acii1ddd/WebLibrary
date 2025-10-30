using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts.Contracts.Books.Requests;
using WebLibrary.BLL.Interfaces;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int? startYear, CancellationToken ct)
    {
        var books = await bookService.GetAllAsync(startYear, ct);
        
        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var author = await bookService.GetByIdAsync(id, ct);
        
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddBookRequest addBookRequest, 
        CancellationToken ct)
    {
        var bookId = await bookService.AddAsync(addBookRequest, ct);
        
        return Created($"/api/books/{bookId}", new {id = bookId});
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateBookRequest updateBookRequest,
        [FromRoute] Guid id, 
        CancellationToken ct)
    {
        await bookService.UpdateAsync(updateBookRequest, id, ct);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await bookService.DeleteAsync(id, ct);

        return NoContent();
    }
}