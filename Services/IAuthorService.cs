
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IAuthorService
    {
        Task<ServiceResponse<AuthorDto>> AddNewAuthor(AuthorDto dto);
        Task<ServiceResponse<AuthorDto>> UpdateAuthor(AuthorDto updateAuthor);
        Task<ServiceResponse<List<AuthorDto>>> GetAllAuthors();
        Task<ServiceResponse<AuthorDto>> GetAuthorById(int id);
    }
}