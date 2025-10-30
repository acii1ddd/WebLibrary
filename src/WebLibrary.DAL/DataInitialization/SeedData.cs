using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.DataInitialization;

public static class SeedData
{
    public static List<Author> GetAuthorsData()
    {
        var authors = new List<Author>();
        
        var author1 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Isaac Asimov",
            DateOfBirth = new DateOnly(1920, 1, 2),
            Books = []
        };

        var author2 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Daniil Kazakov",
            DateOfBirth = new DateOnly(1920, 1, 2),
            Books = []
        };

        var author3 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Jane Austen",
            DateOfBirth = new DateOnly(1775, 12, 16),
            Books = []
        };
        
        authors.AddRange(author1, author2, author3);
        return authors;
    }

    public static List<Book> GetBooksData()
    {
        var books = new List<Book>();
        
        var book1 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Foundation", 
            PublishedYear = 1951, 
            Authors = []
        }; 
        
        var book2 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Pride and Prejudice", 
            PublishedYear = 1813, 
            Authors = []
        };
        
        var book3 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Kafka on the Shore", 
            PublishedYear = 2002, 
            Authors = []
        };
        
        var book4 = new Book
        {
            Id = Guid.NewGuid(), 
            Title = "Kafka on the Shore", 
            PublishedYear = 2002, 
            Authors = []
        };
        
        books.AddRange(book1, book2, book3, book4);
        return books;
    }

    public static void LinkAuthorsWithBooks(List<Book> books, List<Author> authors)
    {
        // авторы
        authors[0].Books.Add(books[0]);
        authors[0].Books.Add(books[1]);
        
        authors[1].Books.Add(books[1]);
        authors[1].Books.Add(books[2]);
        
        authors[2].Books.Add(books[2]);
        authors[2].Books.Add(books[3]);
        
        // книги
        books[0].Authors.Add(authors[0]);
        
        books[1].Authors.Add(authors[0]);
        books[1].Authors.Add(authors[1]);
        
        books[2].Authors.Add(authors[1]);
        books[2].Authors.Add(authors[2]);
        
        books[3].Authors.Add(authors[2]);
    }
}