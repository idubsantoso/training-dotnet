
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IBookAuthorService
    {
        Task<ServiceResponse<BookAuthor>> AddNewBookAuthor(BookAuthor bookAuthor);
        Task<ServiceResponse<BookAuthor>> UpdateBookAuthor(BookAuthor updateAuthor);
        Task<ServiceResponse<List<BookAuthor>>> GetAllBookAuthors();
        Task<ServiceResponse<BookAuthor>> GetBookAuthorById(int id);
    }
}