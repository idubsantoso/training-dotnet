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
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;

        public BookAuthorController(IBookAuthorService bookAuthorService, IBackgroundTaskQueue<BookDto> queue)
        {
            _bookAuthorService = bookAuthorService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<BookAuthor>>>> GetAingleBook(){
            return Ok(await _bookAuthorService.GetAllBookAuthors());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<BookAuthor>>> GetSingleBook(int id){
            return Ok(await _bookAuthorService.GetBookAuthorById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<BookAuthor>>>> AddBook(BookAuthor newBookAuthor)
        {
            return Ok(await _bookAuthorService.AddNewBookAuthor(newBookAuthor));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<BookAuthor>>>> UpdateBook(BookAuthor updateBookAuthor)
        {
            return Ok(await _bookAuthorService.UpdateBookAuthor(updateBookAuthor));
        }

        
    }
}