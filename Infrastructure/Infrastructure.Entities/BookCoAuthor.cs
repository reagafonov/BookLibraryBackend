namespace Infrastructure.Entities;

/// <summary>
/// Дополнительная сущность,
/// для задания настроек реализации отношения многие авторы ко многим книгам в PostgresSql
/// </summary>
public class BookCoAuthor
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public int AuthorId { get; init; }

    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public int BookId { get; init; }
}