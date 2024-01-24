
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICinemaService
    {
        Task<ServiceResponse<CinemaDto>> AddNewCinema(CinemaDto dto);
        Task<ServiceResponse<CinemaDto>> AddNewCinemaWithMovie(CinemaDto dto);
        Task<ServiceResponse<CinemaDto>> UpdateCinema(CinemaDto updateCinema);
        ServiceResponse<List<CinemaStudio>> GetAllCinemaStudios();
        Task<ServiceResponse<CinemaDto>> GetCinemaById(int id);
        CinemaWithMoviesDto GetCinemaWithMovieById(int cinemaId);
        Task<ServiceResponse<CinemaDto>> DeleteCinema(int id);
    }
}