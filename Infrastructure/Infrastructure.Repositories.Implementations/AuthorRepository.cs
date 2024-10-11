using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

/// <summary>
/// Репозиторий работы с авторами
/// </summary>
public class AuthorRepository(
    DatabaseContext context,
    ISimpleFilterQuery<Author, AuthorFilter> authorSimpleFilterQuery)
    : Repository<Author, int>(context), IAuthorRepository
{
    /// <summary>
    /// Получить постраничный список
    /// </summary>
    /// <param name="page">номер страницы</param>
    /// <param name="itemsPerPage">объем страницы</param>
    /// <param name="filter">Фильтр запросов</param>
    /// <returns> Список авторов</returns>
    public async Task<List<Author?>> GetPagedAsync(int page, int itemsPerPage, AuthorFilter filter)
    {
            var query = GetAll();

            query = authorSimpleFilterQuery.Filter(query, filter);

            return (await query.OrderBy(author=>author.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync())!;
        }
}