using System.Collections.Generic;

namespace Services.Contracts
{
    /// <summary>
    /// ДТО книги
    /// </summary>
    public class BookDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Описание книги
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Автор
        /// </summary>
        public AuthorDto MainAuthor { get; init; }

        /// <summary>
        /// Соавторы книги
        /// </summary>
        public ICollection<AuthorDto> CoAuthors { get; init; }
    }
}