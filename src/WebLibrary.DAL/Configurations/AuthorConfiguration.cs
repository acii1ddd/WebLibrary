using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    private const int MaxLength = 128;
    
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("authors");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(MaxLength).IsRequired();
        
        builder.Property(x => x.DateOfBirth)
            .HasMaxLength(MaxLength).IsRequired();
        
        builder.HasMany(x => x.Books)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId);
    }
}