namespace WebLibrary.DAL.Models;

public class Author
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
}