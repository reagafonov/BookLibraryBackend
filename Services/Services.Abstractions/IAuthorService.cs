using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Contracts;

namespace Services.Abstractions
{
    /// <summary>
    /// Сервис работы с авторами (интерфейс)
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns>Список авторов</returns>
        Task<ICollection<AuthorDto>> GetPaged(int page, int pageSize);

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>ДТО автора</returns>
        Task<AuthorDto> GetById(int id);

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="lessonDto">ДТО автора</param>
        /// <returns>идентификатор</returns>
        Task<int> Create(AuthorDto advertisementDto);

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="lessonDto">ДТО автора</param>
        Task Update(int id, AuthorDto advertisementDto);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">идентификатор</param>
        Task Delete(int id);
    }
}