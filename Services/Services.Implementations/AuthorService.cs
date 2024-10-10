using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{
    /// <summary>
    /// Сервис работы с авторами
    /// </summary>
    public class AuthorService(
        IMapper mapper,
        IAuthorRepository authorRepository,
        IValidateDto<AuthorDto> authorDtoValidator) : IAuthorService
    {
        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns>список авторов</returns>
        public async Task<ICollection<AuthorDto>> GetPaged(int page, int pageSize)
        {
            ICollection<Author> entities = await authorRepository.GetPagedAsync(page, pageSize);
            return mapper.Map<ICollection<Author>, ICollection<AuthorDto>>(entities);
        }

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>ДТО автора</returns>
        public async Task<AuthorDto> GetById(int id)
        {
            var author = await authorRepository.GetAsync(id);
            return mapper.Map<AuthorDto>(author);
        }

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="authorDto">ДТО автора</param>
        /// <returns>идентификатор</returns>
        public async Task<int> Create(AuthorDto authorDto)
        {
            authorDtoValidator.Validate(authorDto);
            var entity = mapper.Map<Author>(authorDto);
            var res = await authorRepository.AddAsync(entity);
            await authorRepository.SaveChangesAsync();
            return res.Id;
        }

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="authorDto">ДТО автора</param>
        public async Task Update(int id, AuthorDto authorDto)
        {
            authorDtoValidator.Validate(authorDto);
            var entity = mapper.Map<Author>(authorDto);
            entity.Id = id;
            authorRepository.Update(entity);
            await authorRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">идентификатор</param>
        public async Task Delete(int id)
        {
            var author = await authorRepository.GetAsync(id);
            author.Deleted = true;
            await authorRepository.SaveChangesAsync();
        }
    }
}