using Domain.Entities;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    /// <summary>
    /// Автоматически подключаемая конфигурация для таблицы книг
    /// </summary>
    public void Configure(EntityTypeBuilder<Book> bookBuilder)
    {
        bookBuilder.ToTable("Book", NpgSqlConstants.DefaultSchema);
        bookBuilder.HasKey(book => book.Id);
        bookBuilder.Property(book => book.Id).UseIdentityColumn().HasColumnName("Id");
        bookBuilder.Property(book => book.Title)
            .IsRequired(DomainConstraints.BookTitleIsRequired)
            .HasMaxLength(DomainConstraints.BookTitleMaxLength)
            .HasColumnName("Title");
        bookBuilder.Property(book => book.MainAuthorId)
            .IsRequired(DomainConstraints.BookMainAuthorIdIsRequired)
            .HasColumnName("MainAuthorId");
        bookBuilder.Property(book => book.Deleted)
            .IsRequired()
            .HasColumnName("Deleted")
            .HasDefaultValue(false);

        bookBuilder.Property(book => book.Description)
            .HasMaxLength(DomainConstraints.BookDescriptionMaxLength)
            .IsRequired(DomainConstraints.BookDescriptionIsRequired)
            .HasColumnName("Description");

        bookBuilder.HasQueryFilter(book => !book.Deleted);

        bookBuilder
            .HasMany(u => u.CoAuthors)
            .WithMany(c => c.BookCoAuthors)
            .UsingEntity<BookCoAuthor>(
                author => author.HasOne<Author>().WithMany().HasForeignKey(bookCoAuthor => bookCoAuthor.AuthorId),
                book => book.HasOne<Book>().WithMany().HasForeignKey(bookCoAuthor => bookCoAuthor.BookId),
                builder =>
                {
                    builder.ToTable("BookCoAuthors", NpgSqlConstants.DefaultSchema);
                    builder.HasKey(bookCoAuthor => new { bookCoAuthor.AuthorId, bookCoAuthor.BookId });
                    builder.Property(bookCoAuthor => bookCoAuthor.AuthorId).IsRequired().HasColumnName("AuthorId");
                    builder.Property(bookCoAuthor => bookCoAuthor.BookId).IsRequired().HasColumnName("BookId");
                });

        bookBuilder
            .HasOne(u => u.MainAuthor)
            .WithMany(u => u.BooksAuthor)
            .HasForeignKey(u => u.MainAuthorId)
            .IsRequired(DomainConstraints.BookMainAuthorIdIsRequired);
        bookBuilder.HasIndex(x => x.Title).IsUnique(DomainConstraints.IsTitleUnique);
    }
}