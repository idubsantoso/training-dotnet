
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IBookService
    {
        Task<ServiceResponse<BookDto>> AddNewBook(BookDto dto);
        Task<ServiceResponse<BookDto>> AddNewBookWithAuthor(BookDto dto);
        Task<ServiceResponse<BookDto>> UpdateBook(BookDto updateBook);
        ServiceResponse<List<Book>> GetAllBooks();
        Task<ServiceResponse<BookDto>> GetBookById(int id);
        BookWithAuthorsDto GetBookWithAuthorById(int bookId);
    }
}