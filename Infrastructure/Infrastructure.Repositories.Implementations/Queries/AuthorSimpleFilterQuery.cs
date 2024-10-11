using System.Linq;
using Domain.Entities;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations.Queries;

public class AuthorSimpleFilterQuery : ISimpleFilterQuery<Author, AuthorFilter>
{
    /// <summary>
    /// Добавляет запрос с фильтром по простым полям класса карточки автора
    /// </summary>
    /// <param name="query">Исходный запрос</param>
    /// <param name="filter">Фильтр</param>
    /// <returns>Запрос с фильтр</returns>
    public IQueryable<Author> Filter(IQueryable<Author> query, AuthorFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.FirstName))
            query = query.Where(author => author.FirstName.Contains(filter.FirstName));
        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(author => author.LastName!.Contains(filter.LastName));
        return query;
    }
}