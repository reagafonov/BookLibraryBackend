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
    /// Cервис работы с книгами
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(
            IMapper mapper,
            IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns>список книг</returns>
        public async Task<ICollection<BookDto>> GetPaged(int page, int pageSize)
        {
            ICollection<Book> entities = await _bookRepository.GetPagedAsync(page, pageSize);
            return _mapper.Map<ICollection<Book>, ICollection<BookDto>>(entities);
        }

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>ДТО книги</returns>
        public async Task<BookDto> GetById(int id)
        {
            var course = await _bookRepository.GetAsync(id);
            return _mapper.Map<BookDto>(course);
        }

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="bookDto">ДТО книги</param>
        /// <returns>идентификатор</returns>
        public async Task<int> Create(BookDto bookDto)
        {
            var entity = _mapper.Map<BookDto, Book>(bookDto);
            var res = await _bookRepository.AddAsync(entity);
            await _bookRepository.SaveChangesAsync();
            return res.Id;
        }

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="bookDto">ДТО книги</param>
        public async Task Update(int id, BookDto bookDto)
        {
            var entity = _mapper.Map<BookDto, Book>(bookDto);
            entity.Id = id;
            _bookRepository.Update(entity);
            await _bookRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">идентификатор</param>
        public async Task Delete(int id)
        {
            var course = await _bookRepository.GetAsync(id);
            course.Deleted = true;
            await _bookRepository.SaveChangesAsync();
        }
    }
}