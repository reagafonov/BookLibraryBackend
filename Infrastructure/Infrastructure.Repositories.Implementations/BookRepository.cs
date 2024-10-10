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
    public class BookRepository(DatabaseContext context) : Repository<Book, int>(context), IBookRepository
    {
        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="itemsPerPage">объем страницы</param>
        /// <returns> Список книг</returns>
        public async Task<List<Book>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        // /// <summary>
        // /// Дополнительная инициализация перед добавлением карточки книги
        // /// </summary>
        // /// <param name="entity">Карточка книги</param>
        // /// <returns>Добавленная карточка</returns>
        // public override async Task<Book> AddAsync(Book entity)
        // {
        //     await using var transaction = await context.Database.BeginTransactionAsync();
        //    
        //     var coauthorsLinq = 
        //         entity?.CoAuthors?.Where(x=>x.Id != default) ?? Array.Empty<Author>();
        //     
        //     var coAuthorsList = coauthorsLinq.ToList();
        //     
        //     entity.CoAuthors.Clear();
        //     
        //     var added =await base.AddAsync(entity);
        //     
        //     await SaveChangesAsync();
        //     
        //     var links = coAuthorsList.Select(coAuthor => new BookCoAuthor()
        //         { 
        //             AuthorId = coAuthor.Id, 
        //             BookId = added.Id 
        //         }).ToArray();
        //     
        //     context.AddRange(links);
        //     return added;
        // }
    }
}