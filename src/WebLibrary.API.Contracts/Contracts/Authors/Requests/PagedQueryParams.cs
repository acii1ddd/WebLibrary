using System.ComponentModel.DataAnnotations;

namespace WebLibrary.API.Contracts.Contracts.Authors.Requests;

public class PagedQueryParams
{
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
    
    public void ValidateAndThrow()
    {
        var errors = new List<string>();
        
        if (PageNumber < 1)
        {
            errors.Add("PageNumber must be greater than or equal to 1.");
        }
        
        if (PageSize is < 1 or > 100)
        {
            errors.Add("PageSize must be between 1 and 100.");
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(string.Join(';', errors));
        }
    }
}