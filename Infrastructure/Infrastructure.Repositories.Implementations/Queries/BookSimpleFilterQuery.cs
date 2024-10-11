using System.Linq;
using Domain.Entities;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations.Queries;

/// <summary>
/// Фильтрация по полям класса из фильтра
/// </summary>
public class BookSimpleFilterQuery : ISimpleFilterQuery<Book, BookFilter>
{
    /// <summary>
    /// Добавляет запрос с фильтром по простым полям класса карточки книги
    /// </summary>
    /// <param name="query">Исходный запрос</param>
    /// <param name="filter">Фильтр</param>
    /// <returns>Запрос с фильтр</returns>
    public IQueryable<Book> Filter(IQueryable<Book> query, BookFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(book => book.Title.Contains(filter.Title));
        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(book => book.Description!.Contains(filter.Description));
        return query;
    }
}