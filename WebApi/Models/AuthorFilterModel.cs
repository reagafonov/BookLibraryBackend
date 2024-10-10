namespace WebApi.Models;

/// <summary>
/// Модель фильтра карточек авторов
/// совмещенная с параметрами пагинации
/// </summary>
/// <remarks>Решил не переносить сюда пагинацию</remarks>
public class AuthorFilterModel
{
    /// <summary>
    /// Шаблон фильтра по имени
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Шаблон фильтра по фамилии
    /// </summary>
    public string LastName { get; init; }
}