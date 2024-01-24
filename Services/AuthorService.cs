
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Exceptions;

namespace WebApi.Services
{
    
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AuthorService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<AuthorDto>> AddNewAuthor(AuthorDto dto)
        {
            var serviceResponse = new ServiceResponse<AuthorDto>();

            var newAuthor = new Author()
            {
                Name = dto.Name,
                Address = dto.Address,
                NumberId = dto.NumberId,
            };
            var dbAuthor = await _context.Authors.AddAsync(newAuthor);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<AuthorDto>> AddNewAuthorWithBook(AuthorDto dto)
        {
            var serviceResponse = new ServiceResponse<AuthorDto>();

            var newAuthor = new Author()
            {
                Name = dto.Name,
                Address = dto.Address,
                NumberId = dto.NumberId,
            };
            await _context.Authors.AddAsync(newAuthor);
            await _context.SaveChangesAsync();

            if(dto.BookIds != null){
                foreach(var id in dto.BookIds){
                    var newBookAuthor = new BookAuthor()
                    {
                        BookId = id,
                        AuthorId = newAuthor.Id,
                    };
                    await _context.BookAuthors.AddAsync(newBookAuthor);
                    await _context.SaveChangesAsync();
                }
            }
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<AuthorDto>> UpdateAuthor(AuthorDto updateAuthor)
        {
            var serviceResponse = new ServiceResponse<AuthorDto>();

            try 
            {
                var dbAuthor = await _context.Authors.FirstOrDefaultAsync(c => c.Id == updateAuthor.Id);
                if(dbAuthor is null)
                    throw new NotFoundException($"Author with Id '{updateAuthor.Id}' not found.");
                
                // _mapper.Map(updateCharacter, character);

                dbAuthor.Name = updateAuthor.Name;
                dbAuthor.Address = updateAuthor.Address;
                dbAuthor.NumberId = updateAuthor.NumberId;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<AuthorDto>(dbAuthor);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<AuthorDto>>> GetAllAuthors()
        {
            var serviceResponse = new ServiceResponse<List<AuthorDto>>();
            var dbAuthor = await _context.Authors.ToListAsync();
            serviceResponse.Data = dbAuthor.Select(c => _mapper.Map<AuthorDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<AuthorDto>> GetAuthorById(int id)
        {
            var serviceResponse = new ServiceResponse<AuthorDto>();
            var dbAuthor = await _context.Authors.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<AuthorDto>(dbAuthor);
            return serviceResponse;
        }

        public AuthorWithBooksDto GetAuthorWithBookById(int authorId)
        {
            var serviceResponse = new ServiceResponse<AuthorDto>();
            var bookWithAuthor = _context.Authors.Where(n => n.Id == authorId).Select(author => new AuthorWithBooksDto(){
                Id = author.Id,
                Name = author.Name,
                Address = author.Address,
                NumberId = author.NumberId,
                BookNames = author.BookAuthors.Select(x => x.Book.Title).ToList(),
            }).FirstOrDefault();
            return bookWithAuthor;
        }
    }
}