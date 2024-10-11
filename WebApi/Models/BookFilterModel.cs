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
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Title { get; init; }

    /// <summary>
    /// Часть описания
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Description { get; init; }

    /// <summary>
    /// Поля фильтра по главному автору
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public AuthorFilterModel? MainAuthor { get; init; }

    /// <summary>
    /// Поля для фильтрации по соавторам
    /// </summary>
    /// <remarks>Применяется или</remarks>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public AuthorFilterModel? CoAuthor { get; set; }
}