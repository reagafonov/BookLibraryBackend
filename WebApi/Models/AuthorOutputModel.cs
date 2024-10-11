namespace WebApi.Models;

/// <summary>
/// Модель автора
/// </summary>
public record AuthorOutputModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Фамилия автора
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Имя автора
    /// </summary>
    public required string FirstName { get; init; }
}