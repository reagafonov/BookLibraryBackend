using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Exceptions;
using Services.Abstractions;
using Services.Contracts;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{
    /// <summary>
    /// Cервис работы с книгами
    /// </summary>
    public class BookService(
        IMapper mapper,
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IValidateDto<BookDto> bookValidation) : IBookService
    {
        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <returns>список книг</returns>
        public async Task<ICollection<BookDto>> GetPaged(int page, int pageSize)
        {
            ICollection<Book> entities = await bookRepository.GetPagedAsync(page, pageSize);
            return mapper.Map<ICollection<Book>, ICollection<BookDto>>(entities);
        }

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>ДТО книги</returns>
        public async Task<BookDto> GetById(int id)
        {
            var book = await bookRepository.GetAsync(id);
            return mapper.Map<BookDto>(book);
        }

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="bookDto">ДТО книги</param>
        /// <returns>идентификатор</returns>
        public async Task<int> Create(BookDto bookDto)
        {
            bookValidation.Validate(bookDto);
            var entity = mapper.Map<BookDto, Book>(bookDto);

            if (bookDto.MainAuthor.Id != default)
                entity.MainAuthorId = bookDto.MainAuthor.Id;

            var res = await bookRepository.AddAsync(entity);

            res.CoAuthors ??= new List<Author>();

            //Не добавленные авторы
            var coAuthorsToAdd = bookDto.CoAuthors.Where(coAuthor => coAuthor.Id == default)
                .Select(mapper.Map<Author>).ToList();

            foreach (var author in coAuthorsToAdd)
            {
                var addedAuthor = await authorRepository.AddAsync(author);
                res.CoAuthors.Add(addedAuthor);
            }

            //добавленные авторы
            var coAuthorsIds = bookDto.CoAuthors.Where(coAuthor => coAuthor.Id != default)
                .Select(coAuthor => coAuthor.Id).ToArray();
            foreach (var coAuthorsId in coAuthorsIds)
            {
                var coAuthor = await authorRepository.GetAsync(coAuthorsId);
                if (coAuthor is null)
                    throw new CRUDUpdateException($"Не найден соавтор с ID {coAuthorsId}");
                res.CoAuthors.Add(coAuthor);
            }

            await bookRepository.SaveChangesAsync();

            return res.Id;
        }

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="bookDto">ДТО книги</param>
        public async Task Update(int id, BookDto bookDto)
        {
            bookValidation.Validate(bookDto);
            var entity = mapper.Map<BookDto, Book>(bookDto);
            entity.Id = id;
            bookRepository.Update(entity);
            await bookRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">идентификатор</param>
        public async Task Delete(int id)
        {
            var book = await bookRepository.GetAsync(id);
            book.Deleted = true;
            await bookRepository.SaveChangesAsync();
        }
    }
}