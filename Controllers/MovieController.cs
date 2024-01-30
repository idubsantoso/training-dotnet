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
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any,Duration =10000)]
        [HttpGet]
        public ActionResult<ServiceResponse<List<MovieDto>>> GetAllMovies(){
            return Ok( _movieService.GetAllMovies());
        }


        [HttpGet("{id}")]
        public ActionResult<MovieWithCinemasDto> GetSingleMovieWithCinema(int id){
            return Ok( _movieService.GetMovieWithCinemaById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<MovieDto>>>> AddMovie(MovieDto newMovie)
        {
            return Ok(await _movieService.AddNewMovie(newMovie));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<MovieDto>>>> UpdateMovie(MovieDto updateCinema)
        {
            return Ok(await _movieService.UpdateMovie(updateCinema));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<MovieDto>>>> DeleteMovie(int id)
        {
            return Ok(await _movieService.DeleteMovie(id));
        }
        
    }
}