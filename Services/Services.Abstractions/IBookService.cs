using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Contracts;

namespace Services.Abstractions;

/// <summary>
/// Cервис работы с книгами (интерфейс)
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Получить список
    /// </summary>
    /// <param name="page">номер страницы</param>
    /// <param name="pageSize">объем страницы</param>
    /// <param name="filterDto"></param>
    /// <returns>Список книг</returns>
    Task<ICollection<BookDto>> GetPaged(int page, int pageSize, BookFilterDto filterDto);

    /// <summary>
    /// Получить
    /// </summary>
    /// <param name="id">идентификатор</param>
    /// <returns>ДТО книги</returns>
    Task<BookDto> GetById(int id);

    /// <summary>
    /// Создать
    /// </summary>
    /// <param name="bookDto">ДТО книги</param>
    Task<int> Create(BookDto bookDto);

    /// <summary>
    /// Изменить
    /// </summary>
    /// <param name="id">идентификатор</param>
    /// <param name="bookDto">ДТО книги</param>
    Task Update(int id, BookDto bookDto);

    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="id">идентификатор</param>
    Task Delete(int id);
}