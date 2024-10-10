using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using Services.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
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
        [HttpGet("{id}")]
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
            if (!ModelState.IsValid)
            {
                var modelStateErrors = ModelState.SelectMany(x => x.Value.Errors)
                    .SelectMany(x => x.ErrorMessage);
                var errors = string.Join('\n', modelStateErrors);
                return BadRequest(errors);
            }

            return Ok(await service.Create(mapper.Map<AuthorDto>(authorInputModel)));
        }

        /// <summary>
        /// Редактирование карточки автора
        /// </summary>
        /// <param name="id">Идентификатор карточки автора</param>
        /// <param name="authorInputModel">Новые значения полей карточки автора</param>
        /// <returns>Статус операции</returns>
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation($"Удаление карточки автора по ID {id}");
            await service.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Получение списка авторов 
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Число записей на странице</param>
        /// <returns>Список карточек авторов</returns>
        [HttpGet("list/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetList(int page, int itemsPerPage)
        {
            return Ok(mapper.Map<List<AuthorOutputModel>>(await service.GetPaged(page, itemsPerPage)));
        }
    }
}