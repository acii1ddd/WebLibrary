namespace WebLibrary.API.Contracts.Authors.Requests;

public class UpdateAuthorRequest
{
    public string Name { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
}