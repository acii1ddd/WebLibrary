using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL;

public static class DataInitializer
{
    public static async Task Initialize(LibraryContext context)
    {
        await context.Database.MigrateAsync();
        
        if (context.Authors.Any())
        {
            Console.WriteLine("Уже об авторах уже существуют.");
            return;
        }
        
        if (context.Books.Any())
        {
            Console.WriteLine("Уже о книгах уже существуют.");
            return;
        }
        
        Console.WriteLine("Добавление данных...");
        
        // добавление авторов
        var author1 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Isaac Asimov",
            DateOfBirth = new DateTime(1920, 1, 2),
            Books = []
        };

        var author2 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Daniil Kazakov",
            DateOfBirth = new DateTime(1920, 1, 2),
            Books = []
        };

        var author3 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Jane Austen",
            DateOfBirth = new DateTime(1775, 12, 16),
            Books = []
        };
        
        // добавление книг
        var book1 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Foundation", 
            PublishedYear = 1951, 
            AuthorId = author1.Id
        }; 
        
        var book2 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Pride and Prejudice", 
            PublishedYear = 1813, 
            AuthorId = author1.Id
        };
        
        var book3 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Kafka on the Shore", 
            PublishedYear = 2002, 
            AuthorId = author1.Id
        };
        
        var book4 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Kafka on the Shore", 
            PublishedYear = 2002, 
            AuthorId = author2.Id
        };
        
        var book5 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Kafka on the Shore", 
            PublishedYear = 2002, 
            AuthorId = author3.Id
        };

        author1.Books.Add(book1);
        author1.Books.Add(book2);
        author1.Books.Add(book3);
        
        author2.Books.Add(book4);
        
        author3.Books.Add(book5);
        
        await context.Authors.AddRangeAsync(author1, author2, author3);
        
        await context.Books.AddRangeAsync(book1, book2, book3, book4, book5);
        
        await context.SaveChangesAsync();
    }
}