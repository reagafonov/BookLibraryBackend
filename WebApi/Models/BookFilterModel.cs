namespace WebApi.Models;

/// <summary>
/// Модель фильтра карточек книг
/// </summary>
/// <remarks>Решил не переносить сюда пагинацию</remarks>
public class BookFilterModel
{
    /// <summary>
    /// Часть названия
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Часть описания
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Поля фильтра по главному автору
    /// </summary>
    public AuthorFilterModel? MainAuthor { get; init; } = new();

    /// <summary>
    /// Поля для фильтрации по соавторам
    /// </summary>
    /// <remarks>Применяется или</remarks>
    public AuthorFilterModel CoAuthor { get; set; }
}