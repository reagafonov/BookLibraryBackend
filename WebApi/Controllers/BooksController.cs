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
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private IMapper _mapper;
        private IBookService _service;

        public BooksController(IBookService service, ILogger<BooksController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_mapper.Map<BookModel>(await _service.GetById(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookModel bookModel)
        {
            return Ok(await _service.Create(_mapper.Map<BookDto>(bookModel)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, BookModel bookModel)
        {
            await _service.Update(id, _mapper.Map<BookDto>(bookModel));
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
            return Ok(_mapper.Map<List<BookModel>>(await _service.GetPaged(page, itemsPerPage)));
        }
    }
}