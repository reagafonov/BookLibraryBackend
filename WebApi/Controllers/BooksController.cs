using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using Services.Contracts;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController(IBookService service, ILogger<BooksController> logger, IMapper mapper)
    : ControllerBase
{
    /// <summary>
    /// Получение карточки книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>Карточка книги</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        logger.LogInformation($"Получение карточки книги по ID {id}");
        return Ok(mapper.Map<BookOutputModel>(await service.GetById(id)));
    }

    /// <summary>
    /// Добавление карточки книги
    /// </summary>
    /// <param name="bookInputModel">Карточка книги</param>
    /// <returns>Идентификатор созданной карточки книги</returns>
    [HttpPost]
    public async Task<IActionResult> Add(BookInputModel bookInputModel)
    {
        return Ok(await service.Create(mapper.Map<BookDto>(bookInputModel)));
    }

    /// <summary>
    /// Редактирование карточки книги
    /// </summary>
    /// <param name="id">Идентификатор карточки книги</param>
    /// <param name="bookInputModel">Новые значения полей карточки книги</param>
    /// <returns>Статус операции</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int id, BookInputModel bookInputModel)
    {
        logger.LogInformation($"Редактирование карточки книги по ID {id} данными {bookInputModel}");
        await service.Update(id, mapper.Map<BookDto>(bookInputModel));
        return Ok();
    }

    /// <summary>
    /// Удаление карточки книги
    /// </summary>
    /// <param name="id">Идентификатор карточки</param>
    /// <returns>Статус операции</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation($"Удаление карточки книги по ID {id}");
        await service.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Получение списка книг 
    /// </summary>
    /// <param name="page">Страница</param>
    /// <param name="itemsPerPage">Число записей на странице</param>
    /// <param name="filterModel">Фильтр</param>
    /// <returns>Список карточек книг</returns>
    [HttpPost("list/{page:int}/{itemsPerPage:int}")]
    public async Task<IActionResult> GetList([FromQuery] BookFilterModel filterModel, [FromRoute] int page = 1,
        [FromRoute] int itemsPerPage = 10)
    {
        var filterDto = mapper.Map<BookFilterDto>(filterModel);
        return Ok(mapper.Map<List<BookOutputModel>>(await service.GetPaged(page, itemsPerPage, filterDto)));
    }
}