namespace Services.Abstractions;

/// <summary>
/// DTO фильтра карточек авторов
/// </summary>
public class AuthorFilterDto
{
    /// <summary>
    /// Шаблон поиска по части имени
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Шаблон поиска по части фамилии
    /// </summary>
    public string? LastName { get; init; }
}