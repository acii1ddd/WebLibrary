using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts.Contracts.Authors.Requests;
using WebLibrary.BLL.Interfaces;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var authors = await authorService.GetAllAsync();
        
        return Ok(authors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var author = await authorService.GetByIdAsync(id);
        
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddAuthorRequest addAuthorRequest)
    {
        var authorId = await authorService.AddAsync(addAuthorRequest);
        
        return Created($"api/authors/{authorId}", new {id = authorId});
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateAuthorRequest updateAuthorRequest, 
        [FromRoute] Guid id)
    {
        await authorService.UpdateAsync(updateAuthorRequest, id);
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await authorService.DeleteAsync(id);
        
        return NoContent();
    }

    [HttpGet("books-count")]
    public async Task<IActionResult> GetAuthorsWithBookCount()
    {
        var result = await authorService
            .GetAuthorsWithBookCountsAsync();
        
        return Ok(result);
    }
}