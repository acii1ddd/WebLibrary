namespace WebLibrary.DAL.Models;

public class Author
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateOnly DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; } = [];
}