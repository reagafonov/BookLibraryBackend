namespace Services.Abstractions;

/// <summary>
/// Фильтр книг
/// </summary>
public class BookFilterDto
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
    public AuthorFilterDto? MainAuthor { get; init; }

    /// <summary>
    /// Поля для фильтрации по соавторам
    /// </summary>
    /// <remarks>Применяется или</remarks>
    public AuthorFilterDto[]? CoAuthors { get; init; }
}