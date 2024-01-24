
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Exceptions;

namespace WebApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MovieService(IMapper mapper, DataContext context, IAuthorService authorService)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<MovieDto>> AddNewMovie(MovieDto dto)
        {
            var serviceResponse = new ServiceResponse<MovieDto>();

            var newMovie = new Movie()
            {
                Title = dto.Title,
                Category = dto.Category,
                Image = dto.Image,
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<MovieDto>> UpdateMovie(MovieDto updateMovie)
        {
            var serviceResponse = new ServiceResponse<MovieDto>();

            try 
            {
                var dbMovie = await _context.Movies.FirstOrDefaultAsync(c => c.Id == updateMovie.Id);
                if(dbMovie is null)
                    throw new NotFoundException($"Movie with Id '{updateMovie.Id}' not found.");
                

                dbMovie.Title = updateMovie.Title;
                dbMovie.Category = updateMovie.Category;
                dbMovie.Image = updateMovie.Image;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<MovieDto>(dbMovie);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }

        public ServiceResponse<List<Movie>> GetAllMovies()
        {
            var serviceResponse = new ServiceResponse<List<Movie>>();
            var dbMovie = _context.Movies.ToList();
            serviceResponse.Data = dbMovie;
            return serviceResponse;
        }

        public async Task<ServiceResponse<MovieDto>> GetMovieById(int id)
        {
            var serviceResponse = new ServiceResponse<MovieDto>();
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<MovieDto>(dbMovie);
            return serviceResponse;
        }

        public MovieWithCinemasDto GetMovieWithCinemaById(int id)
        {
            var serviceResponse = new ServiceResponse<MovieDto>();
            var movieWithCinemas = _context.Movies.Where(n => n.Id == id).Select(movie => new MovieWithCinemasDto(){
                Id = movie.Id,
                Title = movie.Title,
                Category = movie.Category,
                Image = movie.Image,
                CinemaStudios = movie.CinemaStudioMovies.Select(x => x.Cinema).ToList(),
            }).FirstOrDefault();
            return movieWithCinemas;
        }

        public async Task<ServiceResponse<MovieDto>> DeleteMovie(int id)
        {
            var serviceResponse = new ServiceResponse<MovieDto>();

            try 
            {
                var dbMovie = await _context.Movies.FirstOrDefaultAsync(c => c.Id == id);
                if(dbMovie is null)
                    throw new NotFoundException($"Movie with Id '{id}' not found.");
                
                _context.Movies.Remove(dbMovie);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<MovieDto>(dbMovie);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }
    }
}