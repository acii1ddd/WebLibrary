using System.ComponentModel.DataAnnotations;

namespace WebLibrary.API.Contracts.Contracts.Books.Requests;

public class AddBookRequest
{
    public string Title { get; set; } = string.Empty;
    
    public int PublishedYear { get; set; }

    public List<Guid> Authors { get; set; } = [];
    
    public void ValidateAndThrow()
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(Title))
        {
            errors.Add("Title cannot be empty");
        }
        
        if (PublishedYear > DateTime.UtcNow.Year)
        {
            errors.Add("PublishedYear cannot be in the future");
        }
        
        if (PublishedYear <= 0)
        {
            errors.Add("PublishedYear cannot be less or equal than 0");
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(string.Join(';', errors));
        }
    }
}