using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    /// <summary>
    /// Контекст
    /// </summary>
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        /// <summary>
        /// Книги
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Авторы
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}