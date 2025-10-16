namespace WebLibrary.API.Contracts.Authors.Requests;

public class AddAuthorRequest
{
    public string Name { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
}
