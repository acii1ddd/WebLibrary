namespace WebLibrary.API.Contracts.Contracts.Authors.Responses;

public class GetAuthorsWithBooksCountResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public int BookCount { get; set; }
}