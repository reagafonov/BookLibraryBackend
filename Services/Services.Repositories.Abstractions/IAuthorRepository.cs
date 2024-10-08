using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория работы с авторами
    /// </summary>
    public interface IAuthorRepository : IRepository<Author, int>
    {
        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="itemsPerPage">объем страницы</param>
        /// <returns> Список книг</returns>
        Task<List<Author>> GetPagedAsync(int page, int itemsPerPage);
    }
}