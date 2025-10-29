namespace WebLibrary.DAL.Models;

public class Book
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public int PublishedYear { get; set; }

    public Author Author { get; set; } = null!;

    public Guid AuthorId { get; set; }
}