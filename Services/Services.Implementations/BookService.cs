﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts;
using Services.Repositories.Abstractions;
using Services.Repositories.Abstractions.Exceptions;

namespace Services.Implementations;

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
    /// <param name="filterDto"></param>
    /// <returns>список книг</returns>
    public async Task<ICollection<BookDto>> GetPaged(int page, int pageSize, BookFilterDto filterDto)
    {
        var filter = mapper.Map<BookFilter>(filterDto);
        ICollection<Book?> entities = await bookRepository.GetPagedAsync(page, pageSize, filter);
        return mapper.Map<ICollection<BookDto>>(entities);
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

        if (bookDto.MainAuthor is not null && bookDto.MainAuthor.Id != 0)
            entity.MainAuthorId = bookDto.MainAuthor!.Id;

        var res = await bookRepository.AddAsync(entity);

        await ProcessCoAuthors(bookDto, res);

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
        var book = await bookRepository.GetAsync(id);
        if (book is null)
            throw new CrudUpdateException($"Не найдена книга с id {id}");
        mapper.Map(bookDto, book);
        book.CoAuthors?.Clear();
        await ProcessCoAuthors(bookDto, book);
        await bookRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="id">идентификатор</param>
    public async Task Delete(int id)
    {
        var book = await bookRepository.GetAsync(id);
        if (book is null)
            throw new CrudUpdateException("Книга не найдена");
        book.Deleted = true;
        await bookRepository.SaveChangesAsync();
    }

    private async Task ProcessCoAuthors(BookDto bookDto, Book res)
    {
        res.CoAuthors ??= new List<Author>();

        //Не добавленные авторы
        var coAuthorsToAdd = bookDto.CoAuthors?.Where(coAuthor => coAuthor.Id == default)
            .Select(mapper.Map<Author>).ToList();

        if (coAuthorsToAdd is not null)
            foreach (var author in coAuthorsToAdd)
            {
                var addedAuthor = await authorRepository.AddAsync(author);
                res.CoAuthors.Add(addedAuthor);
            }

        //добавленные авторы
        var coAuthorsIds = bookDto.CoAuthors?.Where(coAuthor => coAuthor.Id != default)
            .Select(coAuthor => coAuthor.Id).ToArray();
        if (coAuthorsIds is not null)
            foreach (var coAuthorsId in coAuthorsIds)
            {
                var coAuthor = await authorRepository.GetAsync(coAuthorsId);
                if (coAuthor is null)
                    throw new CrudUpdateException($"Не найден соавтор с ID {coAuthorsId}");
                res.CoAuthors.Add(coAuthor);
            }
    }
}