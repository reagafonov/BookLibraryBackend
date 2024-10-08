using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Модель курса
    /// </summary>
    public class Book : IEntity<int>
    {
        /// <summary>
        /// Название книги
        /// </summary>
        [StringLength(500)]
        public string Title { get; init; }

        /// <summary>
        /// Описание книги
        /// </summary>
        [StringLength(2000)]
        public string Description { get; init; }

        /// <summary>
        /// Идентификатор главного автора книги
        /// </summary>
        public int MainAuthorID { get; init; }

        /// <summary>
        /// Автор
        /// </summary>
        public virtual Author MainAuthor { get; init; }

        /// <summary>
        /// Соавторы книги
        /// </summary>
        public virtual ICollection<Author>? CoAuthors { get; init; }

        /// <summary>
        /// Удалено
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        public int Id { get; set; }
    }
}