using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

/// <summary>
/// Модель книги
/// </summary>
public record BookInputModel
{
    /// <summary>
    /// Название книги
    /// </summary>
    [Required(ErrorMessage = "Не указано поле Title")]
    [MaxLength(500,ErrorMessage = "Превышена длина поля Title. Максимальная длина 500")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Title { get; init; }

    /// <summary>
    /// Описание книги
    /// </summary>
    [MaxLength(2000,ErrorMessage = "Превышена длина поля Title. Максимальная длина 2000")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Description { get; init; }

    /// <summary>
    /// Автор
    /// </summary>
    [Required(ErrorMessage = "Не указано поле MainAuthorId")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int? MainAuthorID { get; init; }

    /// <summary>
    /// Соавторы книги
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<int>? CoAuthorsIDs { get; init; }
}