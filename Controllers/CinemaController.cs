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
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any,Duration =10000)]
        [HttpGet]
        public ActionResult<ServiceResponse<List<CinemaDto>>> GetAllCinema(){
            return Ok( _cinemaService.GetAllCinemaStudios());
        }


        [HttpGet("{id}")]
        public ActionResult<CinemaWithMoviesDto> GetSingleBookWithAuthor(int id){
            return Ok( _cinemaService.GetCinemaWithMovieById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CinemaDto>>>> AddCinemaWithMovie(CinemaDto newCinema)
        {
            return Ok(await _cinemaService.AddNewCinemaWithMovie(newCinema));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<CinemaDto>>>> UpdateCinema(CinemaDto updateCinema)
        {
            return Ok(await _cinemaService.UpdateCinema(updateCinema));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<CinemaDto>>>> DeleteCinema(int id)
        {
            return Ok(await _cinemaService.DeleteCinema(id));
        }
        
    }
}