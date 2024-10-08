using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория работы с книгами
    /// </summary>
    public interface IBookRepository : IRepository<Book, int>
    {
        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="itemsPerPage">объем страницы</param>
        /// <returns> Список книг</returns>
        Task<List<Book>> GetPagedAsync(int page, int itemsPerPage);
    }
}