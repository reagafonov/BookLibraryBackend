using System.Collections.Generic;

namespace WebApi.Models
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public record BookOutputModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; init; }

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
        public AuthorOutputModel MainAuthor { get; init; }

        /// <summary>
        /// Соавторы книги
        /// </summary>
        public List<AuthorOutputModel> CoAuthors { get; init; }
    }
}