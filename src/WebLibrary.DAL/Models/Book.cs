namespace WebLibrary.DAL.Models;

public class Book
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public int PublishedYear { get; set; }

    public ICollection<Author> Authors { get; set; } = [];
}