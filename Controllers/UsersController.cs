namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Dto;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<UserDto>>>> AddUser(UserDto newUser)
    {
        return Ok(await _userService.CreateNewUser(newUser));
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetAll()
    {
        return Ok(await _userService.GetAll());
    }
}
