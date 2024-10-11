using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий работы с книгами
    /// </summary>
    public class BookRepository(
        DatabaseContext context,
        ISimpleFilterQuery<Book, BookFilter> bookSimpleFilter,
        ISimpleFilterQuery<Author, AuthorFilter> authorQuery) : Repository<Book, int>(context), IBookRepository
    {
        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="itemsPerPage">объем страницы</param>
        /// <param name="filter"></param>
        /// <returns> Список книг</returns>
        public async Task<List<Book>> GetPagedAsync(int page, int itemsPerPage, BookFilter filter)
        {
            var query = GetAll();

            query = bookSimpleFilter.Filter(query, filter);

            if (filter.MainAuthor is not null)
            {
                var mainAuthors = context.Authors.AsQueryable();
                var mainAuthorsIds = authorQuery.Filter(mainAuthors, filter.MainAuthor).Select(x => x.Id);
                query = from book in query
                    join mainAuthorsId in mainAuthorsIds on book.MainAuthorId equals mainAuthorsId
                    select book;
            }

            if (filter.CoAuthors is not null && filter.CoAuthors.Length > 0)
            {
                var mainAuthors = context.Authors.AsQueryable();
                var mainAuthorsIds = authorQuery.Filter(mainAuthors, filter.CoAuthors.First()).Select(x => x.Id);
                foreach (var authorFilter in filter.CoAuthors.Skip(1))
                {
                    mainAuthorsIds =
                        mainAuthorsIds.Intersect(authorQuery.Filter(mainAuthors, authorFilter).Select(x => x.Id));
                }

                query = from book in query
                    from coAuthor in book.CoAuthors
                    join mainAuthorsId in mainAuthorsIds on coAuthor.Id equals mainAuthorsId
                    select book;
            }

            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }
    }
}