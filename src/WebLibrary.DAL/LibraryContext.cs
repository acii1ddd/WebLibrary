using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL;

public class LibraryContext(DbContextOptions<LibraryContext> options) 
    : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);
    }
}