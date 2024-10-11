using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Автор
/// </summary>
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class Author : IEntity<int>
{
    /// <summary>
    /// Фамилия автора
    /// </summary>
    [StringLength(500)]
    [Required]
    public string? LastName { get; init; }

    /// <summary>
    /// Имя автора
    /// </summary>
    [StringLength(500)]
    [Required]
    public required string FirstName { get; init; }

    /// <summary>
    /// Книги автора
    /// </summary>
    public virtual required ICollection<Book> BooksAuthor { get; init; }

    /// <summary>
    /// Книги, в которых автор был соавтором
    /// </summary>
    public virtual required ICollection<Book> BookCoAuthors { get; init; }

    /// <summary>
    /// Удалено
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
}