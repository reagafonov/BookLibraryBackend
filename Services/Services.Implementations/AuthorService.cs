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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(
            IMapper mapper,
            IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns>список авторов</returns>
        public async Task<ICollection<AuthorDto>> GetPaged(int page, int pageSize)
        {
            ICollection<Author> entities = await _authorRepository.GetPagedAsync(page, pageSize);
            return _mapper.Map<ICollection<Author>, ICollection<AuthorDto>>(entities);
        }

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>ДТО автора</returns>
        public async Task<AuthorDto> GetById(int id)
        {
            var lesson = await _authorRepository.GetAsync(id);
            return _mapper.Map<AuthorDto>(lesson);
        }

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="authorDto">ДТО автора</param>
        /// <returns>идентификатор</returns>
        public async Task<int> Create(AuthorDto authorDto)
        {
            var entity = _mapper.Map<Author>(authorDto);
            var res = await _authorRepository.AddAsync(entity);
            await _authorRepository.SaveChangesAsync();
            return res.Id;
        }

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="authorDto">ДТО автора</param>
        public async Task Update(int id, AuthorDto authorDto)
        {
            var entity = _mapper.Map<Author>(authorDto);
            entity.Id = id;
            _authorRepository.Update(entity);
            await _authorRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">идентификатор</param>
        public async Task Delete(int id)
        {
            var lesson = await _authorRepository.GetAsync(id);
            lesson.Deleted = true;
            await _authorRepository.SaveChangesAsync();
        }
    }
}