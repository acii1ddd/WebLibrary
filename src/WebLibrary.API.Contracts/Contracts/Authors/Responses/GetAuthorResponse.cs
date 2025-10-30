namespace WebLibrary.API.Contracts.Contracts.Authors.Responses;

public class GetAuthorResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
}