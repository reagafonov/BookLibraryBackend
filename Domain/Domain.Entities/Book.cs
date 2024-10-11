using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// Модель курса
/// </summary>
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class Book : IEntity<int>
{
    /// <summary>
    /// Название книги
    /// </summary>
    [StringLength(500)]
    public required string Title { get; init; }

    /// <summary>
    /// Описание книги
    /// </summary>
    [StringLength(2000)]
    public string? Description { get; init; }

    /// <summary>
    /// Идентификатор главного автора книги
    /// </summary>
    [Required]
    public int? MainAuthorId { get; set; }

    /// <summary>
    /// Автор
    /// </summary>
    public virtual Author? MainAuthor { get; init; }

    /// <summary>
    /// Соавторы книги
    /// </summary>
    public virtual ICollection<Author>? CoAuthors { get; set; }

    /// <summary>
    /// Удалено
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// Книга
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}