namespace WebLibrary.API.Contracts.Contracts.Books.Requests;

public class UpdateBookRequest
{
    public string Title { get; set; } = string.Empty;
    
    public int PublishedYear { get; set; }
}