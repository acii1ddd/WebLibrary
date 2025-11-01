using System.ComponentModel.DataAnnotations;

namespace WebLibrary.API.Contracts.Contracts.Authors.Requests;

public class UpdateAuthorRequest
{
    public string Name { get; set; } = string.Empty;
    
    public DateOnly DateOfBirth { get; set; }
    
    public void ValidateAndThrow()
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add("Name cannot be empty");
        }
        
        if (DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors.Add("Date of birth cannot be in the future");
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(string.Join(';', errors));
        }
    }
}