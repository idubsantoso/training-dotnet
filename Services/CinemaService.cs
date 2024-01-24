
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Exceptions;

namespace WebApi.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CinemaService(IMapper mapper, DataContext context, IAuthorService authorService)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<CinemaDto>> AddNewCinema(CinemaDto dto)
        {
            var serviceResponse = new ServiceResponse<CinemaDto>();

            var newCinemaStudio = new CinemaStudio()
            {
                Name = dto.Name,
                Address = dto.Address,
                Schedule = dto.Schedule,
                Theaters = dto.Theaters,
            };
            await _context.CinemaStudios.AddAsync(newCinemaStudio);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CinemaDto>> AddNewCinemaWithMovie(CinemaDto dto)
        {
            var serviceResponse = new ServiceResponse<CinemaDto>();

            var newCinemaStudio = new CinemaStudio()
            {
                Name = dto.Name,
                Address = dto.Address,
                Schedule = dto.Schedule,
                Theaters = dto.Theaters,
            };
            await _context.CinemaStudios.AddAsync(newCinemaStudio);
            await _context.SaveChangesAsync();
            if(dto.MovieIds != null){
                foreach(var id in dto.MovieIds){
                    var newCinemaStudioMovie = new CinemaStudioMovie()
                    {
                        CinemaId = newCinemaStudio.Id,
                        MovieId = id,
                        Schedule = newCinemaStudio.Schedule
                    };
                    await _context.CinemaStudioMovies.AddAsync(newCinemaStudioMovie);
                    await _context.SaveChangesAsync();
                }
            }
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CinemaDto>> UpdateCinema(CinemaDto updateCinema)
        {
            var serviceResponse = new ServiceResponse<CinemaDto>();

            try 
            {
                var dbCinema = await _context.CinemaStudios.FirstOrDefaultAsync(c => c.Id == updateCinema.Id);
                if(dbCinema is null)
                    throw new NotFoundException($"Cinema with Id '{updateCinema.Id}' not found.");
                

                dbCinema.Name = updateCinema.Name;
                dbCinema.Address = updateCinema.Address;
                dbCinema.Schedule = updateCinema.Schedule;
                dbCinema.Theaters = updateCinema.Theaters;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<CinemaDto>(dbCinema);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }

        public ServiceResponse<List<CinemaStudio>> GetAllCinemaStudios()
        {
            var serviceResponse = new ServiceResponse<List<CinemaStudio>>();
            var dbCinema = _context.CinemaStudios.ToList();
            serviceResponse.Data = dbCinema;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CinemaDto>> GetCinemaById(int id)
        {
            var serviceResponse = new ServiceResponse<CinemaDto>();
            var dbCinema = await _context.CinemaStudios.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<CinemaDto>(dbCinema);
            return serviceResponse;
        }

        public CinemaWithMoviesDto GetCinemaWithMovieById(int cinemaId)
        {
            var serviceResponse = new ServiceResponse<CinemaDto>();
            var cinemaWithMovies = _context.CinemaStudios.Where(n => n.Id == cinemaId).Select(cinema => new CinemaWithMoviesDto(){
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address,
                Schedule = cinema.Schedule,
                Theaters = cinema.Theaters,
                Movies = cinema.CinemaStudioMovies.Select(x => x.Movie).ToList(),
            }).FirstOrDefault();
            return cinemaWithMovies;
        }

        public async Task<ServiceResponse<CinemaDto>> DeleteCinema(int id)
        {
            var serviceResponse = new ServiceResponse<CinemaDto>();

            try 
            {
                var dbCinema = await _context.CinemaStudios.FirstOrDefaultAsync(c => c.Id == id);
                if(dbCinema is null)
                    throw new NotFoundException($"Cinema with Id '{id}' not found.");
                
                _context.CinemaStudios.Remove(dbCinema);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<CinemaDto>(dbCinema);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }
    }
}