
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IMovieService
    {
        Task<ServiceResponse<MovieDto>> AddNewMovie(MovieDto dto);
        Task<ServiceResponse<MovieDto>> UpdateMovie(MovieDto dto);
        ServiceResponse<List<Movie>> GetAllMovies();
        Task<ServiceResponse<MovieDto>> GetMovieById(int id);
        MovieWithCinemasDto GetMovieWithCinemaById(int id, DateTime schedule);
        Task<ServiceResponse<MovieDto>> DeleteMovie(int id);
    }
}