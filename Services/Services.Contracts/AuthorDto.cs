namespace Services.Contracts;

/// <summary>
/// ДТО автора
/// </summary>
public class AuthorDto
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Фамилия автора
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Имя автора
    /// </summary>
    public string? FirstName { get; init; }
}