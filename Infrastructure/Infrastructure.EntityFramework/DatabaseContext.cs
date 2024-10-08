using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    /// <summary>
    /// Контекст
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Книги
        /// </summary>
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasMany(u => u.CoAuthors)
                .WithMany(c => c.BooksCoAuthor);

            modelBuilder.Entity<Book>()
                .HasOne(u => u.MainAuthor)
                .WithMany(u => u.BooksAuthor)
                .HasForeignKey(u => u.MainAuthorID)
                .IsRequired();
        }
    }
}