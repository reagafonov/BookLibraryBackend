using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using Services.Contracts;
using WebApi.Models;

namespace WebApi.Controllers;

/// <summary>
/// CRUD для авторов
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthorsController(IAuthorService service, ILogger<AuthorsController> logger, IMapper mapper)
    : ControllerBase
{
    /// <summary>
    /// Получение карточки автора
    /// </summary>
    /// <param name="id">Идентификатор автора</param>
    /// <returns>Карточка автора</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        logger.LogInformation($"Получение карточки автора по ID {id}");
        return Ok(mapper.Map<AuthorOutputModel>(await service.GetById(id)));
    }

    /// <summary>
    /// Добавление карточки автора
    /// </summary>
    /// <param name="authorInputModel">Карточка автора</param>
    /// <returns>Идентификатор созданной карточки автора</returns>
    [HttpPost]
    public async Task<IActionResult> Add(AuthorInputModel authorInputModel)
    {
        return Ok(await service.Create(mapper.Map<AuthorDto>(authorInputModel)));
    }

    /// <summary>
    /// Редактирование карточки автора
    /// </summary>
    /// <param name="id">Идентификатор карточки автора</param>
    /// <param name="authorInputModel">Новые значения полей карточки автора</param>
    /// <returns>Статус операции</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int id, AuthorInputModel authorInputModel)
    {
        logger.LogInformation($"Редактирование карточки автора по ID {id} данными {authorInputModel}");
        await service.Update(id, mapper.Map<AuthorDto>(authorInputModel));
        return Ok();
    }

    /// <summary>
    /// Удаление карточки автора
    /// </summary>
    /// <param name="id">Идентификатор карточки</param>
    /// <returns>Статус операции</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation($"Удаление карточки автора по ID {id}");
        await service.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Получение списка авторов 
    /// </summary>
    /// <param name="filter">фильтр</param>
    /// <param name="page">номер страницы</param>
    /// <param name="itemsPerPage">число записей на странице</param>
    /// <returns>Список карточек авторов</returns>
    [HttpGet("list/{page:int}/{itemsPerPage:int}/")]
    public async Task<IActionResult> GetList([FromQuery] AuthorFilterModel filter, [FromRoute] int page = 1,
        [FromRoute] int itemsPerPage = 10)
    {
        var filterDto = mapper.Map<AuthorFilterDto>(filter);
        return Ok(mapper.Map<List<AuthorOutputModel>>(await service.GetPaged(page, itemsPerPage, filterDto)));
    }
}