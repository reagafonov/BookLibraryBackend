using System.Collections.Generic;
using Services.Contracts;

namespace WebApi.Models
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public class BookModel
    {
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
        public List<AuthorDto> CoAuthors { get; init; }
    }
}