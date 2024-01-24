namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Entities;
using WebApi.Models;

public interface IUserService
{
    Task<ServiceResponse<UserDto>> CreateNewUser(UserDto dto);
    AuthenticateResponse? Authenticate(AuthenticateRequest model);
    Task<ServiceResponse<List<UserDto>>> GetAll();
    User? GetById(int id);
}

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
    };

    private readonly IJwtUtils _jwtUtils;

    public UserService(IJwtUtils jwtUtils, IMapper mapper, DataContext context)
    {
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<UserDto>> CreateNewUser(UserDto dto)
        {
            var serviceResponse = new ServiceResponse<UserDto>();

            // var book = _mapper.Map<Book>(dto);
            var newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Password = BCrypt.HashPassword(dto.Password)
            };
            var dbUser = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        // var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

        // validate or user not found return null
        if (user == null || !BCrypt.Verify(model.Password, user.Password)) return null;

        // authentication successful so generate jwt token
        var token = _jwtUtils.GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public async Task<ServiceResponse<List<UserDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<UserDto>>();
        var dbUser = await _context.Users.ToListAsync();
        serviceResponse.Data = dbUser.Select(c => _mapper.Map<UserDto>(c)).ToList();
        return serviceResponse;
    }

    public User? GetById(int id)
    {
        // return _users.FirstOrDefault(x => x.Id == id);
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

}