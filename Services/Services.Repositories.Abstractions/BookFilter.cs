#nullable enable
namespace Services.Repositories.Abstractions;

public class BookFilter
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
    public AuthorFilter? MainAuthor { get; init; } = new();

    /// <summary>
    /// Поля для фильтрации по соавторам
    /// </summary>
    /// <remarks>Применяется или</remarks>
    public AuthorFilter[]? CoAuthors { get; init; }
}