using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    private const int MaxLength = 128;
    
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(MaxLength).IsRequired();
        
        builder.Property(x => x.PublishedYear).IsRequired();
        
        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId);
    }
}