using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Authorization;
using WebApi.Dto;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<AuthorDto>>>> GetAllAuthor(){
            return Ok(await _authorService.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AuthorDto>>> GetSingleAuthor(int id){
            return Ok(await _authorService.GetAuthorById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AuthorDto>>>> AddAuthor(AuthorDto newAuthor)
        {
            return Ok(await _authorService.AddNewAuthor(newAuthor));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<AuthorDto>>>> UpdateAuthor(AuthorDto updateAuthor)
        {
            return Ok(await _authorService.UpdateAuthor(updateAuthor));
        }

        
    }
}