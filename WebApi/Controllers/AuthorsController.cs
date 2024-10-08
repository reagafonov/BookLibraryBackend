using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using Services.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;
        private IAuthorService _service;

        public AuthorsController(IAuthorService service, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_mapper.Map<AuthorModel>(await _service.GetById(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuthorModel authorDto)
        {
            return Ok(await _service.Create(_mapper.Map<AuthorDto>(authorDto)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AuthorModel authorDto)
        {
            await _service.Update(id, _mapper.Map<AuthorDto>(authorDto));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpGet("list/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetList(int page, int itemsPerPage)
        {
            return Ok(_mapper.Map<List<AuthorModel>>(await _service.GetPaged(page, itemsPerPage)));
        }
    }
}