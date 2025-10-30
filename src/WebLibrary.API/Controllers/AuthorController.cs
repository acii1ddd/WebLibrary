using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.BLL.Interfaces;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? name, CancellationToken ct)
    {
        var authors = await authorService.GetAllAsync(name, ct);
        
        return Ok(authors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var author = await authorService.GetByIdAsync(id, ct);
        
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddAuthorRequest addAuthorRequest, 
        CancellationToken ct)
    {
        var authorId = await authorService.AddAsync(addAuthorRequest, ct);
        
        return Created($"api/authors/{authorId}", new {id = authorId});
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateAuthorRequest updateAuthorRequest, 
        [FromRoute] Guid id, 
        CancellationToken ct)
    {
        await authorService.UpdateAsync(updateAuthorRequest, id, ct);
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await authorService.DeleteAsync(id, ct);
        
        return NoContent();
    }

    [HttpGet("books-count")]
    public async Task<IActionResult> GetAuthorsWithBookCount(CancellationToken ct)
    {
        var result = await authorService
            .GetAuthorsWithBookCountsAsync(ct);
        
        return Ok(result);
    }
}