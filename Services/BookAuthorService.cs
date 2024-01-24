
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Exceptions;

namespace WebApi.Services
{
    
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public BookAuthorService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<BookAuthor>> AddNewBookAuthor(BookAuthor bookAuthor)
        {
            var serviceResponse = new ServiceResponse<BookAuthor>();

            var newBookAuthor = new BookAuthor()
            {
                BookId = bookAuthor.BookId,
                AuthorId = bookAuthor.AuthorId,
            };
            await _context.BookAuthors.AddAsync(newBookAuthor);
            await _context.SaveChangesAsync();
            serviceResponse.Data = bookAuthor;
            return serviceResponse;
        }

        public async Task<ServiceResponse<BookAuthor>> UpdateBookAuthor(BookAuthor updateAuthor)
        {
            var serviceResponse = new ServiceResponse<BookAuthor>();

            try 
            {
                var dbBookAuthor = await _context.BookAuthors.FirstOrDefaultAsync(c => c.Id == updateAuthor.Id);
                if(dbBookAuthor is null)
                    throw new NotFoundException($"Author with Id '{updateAuthor.Id}' not found.");
                
                // _mapper.Map(updateCharacter, character);

                dbBookAuthor.BookId = updateAuthor.BookId;
                dbBookAuthor.AuthorId = updateAuthor.AuthorId;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<BookAuthor>(dbBookAuthor);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<BookAuthor>>> GetAllBookAuthors()
        {
            var serviceResponse = new ServiceResponse<List<BookAuthor>>();
            var dbAuthor = await _context.Authors.ToListAsync();
            serviceResponse.Data = dbAuthor.Select(c => _mapper.Map<BookAuthor>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<BookAuthor>> GetBookAuthorById(int id)
        {
            var serviceResponse = new ServiceResponse<BookAuthor>();
            var dbAuthor = await _context.Authors.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<BookAuthor>(dbAuthor);
            return serviceResponse;
        }
    }
}