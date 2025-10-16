using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts.Authors.Requests;
using WebLibrary.BLL.Interfaces;
using WebLibrary.DAL.Models;

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
        
        if (author is null)
            return NotFound();
        
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddAuthorRequest entity)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            DateOfBirth = entity.DateOfBirth
        };
        
        await authorService.AddAsync(author);
        
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateAuthorRequest entity, 
        [FromRoute] Guid id)
    {
        var author = new Author
        {
            Id = id,
            Name = entity.Name,
            DateOfBirth = entity.DateOfBirth
        };
        
        await authorService.UpdateAsync(author);
        
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await authorService.DeleteAsync(id);
        
        return Ok();
    }
}