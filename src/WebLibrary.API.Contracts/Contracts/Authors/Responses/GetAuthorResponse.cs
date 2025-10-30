namespace WebLibrary.API.Contracts.Contracts.Authors.Responses;

public class GetAuthorResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateOnly DateOfBirth { get; set; }
}