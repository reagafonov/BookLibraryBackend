namespace WebApi.Models;

/// <summary>
/// Модель фильтра карточек авторов
/// </summary>
/// <remarks>Решил не переносить сюда пагинацию</remarks>
public class AuthorFilterModel
{
    /// <summary>
    /// Шаблон фильтра по имени
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? FirstName { get; init; }

    /// <summary>
    /// Шаблон фильтра по фамилии
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? LastName { get; init; }
}