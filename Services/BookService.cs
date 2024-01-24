
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Exceptions;

namespace WebApi.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public BookService(IMapper mapper, DataContext context, IAuthorService authorService)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<BookDto>> AddNewBook(BookDto dto)
        {
            var serviceResponse = new ServiceResponse<BookDto>();

            // var book = _mapper.Map<Book>(dto);
            var newBook = new Book()
            {
                Description = dto.Description,
                Title = dto.Title,
                Category = dto.Category,
                TotalPages = dto.TotalPages.HasValue ? dto.TotalPages.Value : 0,
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<BookDto>> AddNewBookWithAuthor(BookDto dto)
        {
            var serviceResponse = new ServiceResponse<BookDto>();

            // var book = _mapper.Map<Book>(dto);
            var newBook = new Book()
            {
                Description = dto.Description,
                Title = dto.Title,
                Category = dto.Category,
                TotalPages = dto.TotalPages.HasValue ? dto.TotalPages.Value : 0,
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            if(dto.AuthorIds != null){
                foreach(var id in dto.AuthorIds){
                    var newBookAuthor = new BookAuthor()
                    {
                        BookId = newBook.Id,
                        AuthorId = id,
                    };
                    await _context.BookAuthors.AddAsync(newBookAuthor);
                    await _context.SaveChangesAsync();
                }
            }
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<BookDto>> UpdateBook(BookDto updateBook)
        {
            var serviceResponse = new ServiceResponse<BookDto>();

            try 
            {
                var dbBook = await _context.Books.FirstOrDefaultAsync(c => c.Id == updateBook.Id);
                if(dbBook is null)
                    throw new NotFoundException($"Character with Id '{updateBook.Id}' not found.");
                
                // _mapper.Map(updateCharacter, character);

                dbBook.Description = updateBook.Description;
                dbBook.Title = updateBook.Title;
                dbBook.Category = updateBook.Category;
                dbBook.TotalPages = updateBook.TotalPages;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<BookDto>(dbBook);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }

        public ServiceResponse<List<Book>> GetAllBooks()
        {
            var serviceResponse = new ServiceResponse<List<Book>>();
            var dbBook = _context.Books.Include(x => x.BookAuthors).ToList();
            serviceResponse.Data = dbBook;
            return serviceResponse;
        }

        public async Task<ServiceResponse<BookDto>> GetBookById(int id)
        {
            var serviceResponse = new ServiceResponse<BookDto>();
            var dbBook = await _context.Books.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<BookDto>(dbBook);
            return serviceResponse;
        }

        public BookWithAuthorsDto GetBookWithAuthorById(int bookId)
        {
            var serviceResponse = new ServiceResponse<BookDto>();
            var bookWithAuthor = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsDto(){
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                TotalPages = book.TotalPages,
                AuthorNames = book.BookAuthors.Select(x => x.Author.Name).ToList(),
            }).FirstOrDefault();
            return bookWithAuthor;
        }
    }
}