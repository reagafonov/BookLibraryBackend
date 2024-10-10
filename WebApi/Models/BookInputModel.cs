using System.Collections.Generic;

namespace WebApi.Models
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public record BookInputModel
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
        public int MainAuthorID { get; init; }

        /// <summary>
        /// Соавторы книги
        /// </summary>
        public List<int> CoAuthorsIDs { get; init; }
    }
}