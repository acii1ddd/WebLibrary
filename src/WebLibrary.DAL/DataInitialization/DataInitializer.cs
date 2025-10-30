using Microsoft.EntityFrameworkCore;

namespace WebLibrary.DAL.DataInitialization;

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

        var authors = SeedData.GetAuthorsData();

        var books = SeedData.GetBooksData();

        SeedData.LinkAuthorsWithBooks(books, authors);

        await context.Authors.AddRangeAsync(authors);

        await context.Books.AddRangeAsync(books);

        await context.SaveChangesAsync();
    }
}