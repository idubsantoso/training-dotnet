using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Authorization;
using WebApi.Dto;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Queue;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        private readonly IBackgroundTaskQueue<BookDto> _queue;
        public BookController(IBookService bookService, IBackgroundTaskQueue<BookDto> queue)
        {
            _bookService = bookService;
            _queue = queue;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any,Duration =10000)]
        [HttpGet]
        public ActionResult<ServiceResponse<List<BookDto>>> GetAllBook(){
            return Ok( _bookService.GetAllBooks());
        }


        [HttpGet("{id}")]
        public ActionResult<BookWithAuthorsDto> GetSingleBookWithAuthor(int id){
            return Ok( _bookService.GetBookWithAuthorById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<BookDto>>>> AddBookWithAuthor(BookDto newBook)
        {
            return Ok(await _bookService.AddNewBookWithAuthor(newBook));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<BookDto>>>> UpdateBook(BookDto updateBook)
        {
            return Ok(await _bookService.UpdateBook(updateBook));
        }

        [HttpPost("queue")]
        public Task<IActionResult> SaveBook([FromBody] List<BookDto> books)
        {
            for(int i = 0; i < books.Capacity; i++){
                _queue.Enqueue(books[i]);
            }
            
            return Task.FromResult<IActionResult>(Accepted());
        }
        
    }
}