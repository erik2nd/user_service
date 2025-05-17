using Microsoft.AspNetCore.Mvc;
using UsersService.BLL.Interfaces;
using UsersService.PL.Models;
using UsersService.PL.Models.Requests;
using UsersService.PL.Models.Responses;

namespace UsersService.PL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, 
        [FromHeader(Name = "X-Login")] string requesterLogin)
    {
        var response = await _userService.CreateUserAsync(request, requesterLogin, new CancellationToken());
        return Ok(response);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllActiveUsers([FromHeader(Name = "X-Login")] string requesterLogin)
    {
        var users = await _userService.GetAllActiveUsersAsync(requesterLogin, new CancellationToken());
        return Ok(users);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<UserByLoginResponse>> GetUserByLogin(
        [FromQuery] string login,
        [FromHeader(Name = "X-Login")] string requesterLogin,
        CancellationToken token)
    {
        var result = await _userService.GetUserByLoginAsync(requesterLogin, login, token);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<UserByLoginAndPasswordResponse>> GetUserByLoginAndPassword(
        [FromBody] GetUserByLoginAndPasswordRequest request,
        [FromHeader(Name = "X-Login")] string requesterLogin,
        CancellationToken token)
    {
        var result = await _userService.GetUserByLoginAndPassword(requesterLogin, request.Login, request.Password, token);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<UserByAgeResponse[]>> GetUsersOlderThanAge(
        [FromBody] UsersByAgeRequest request,
        [FromHeader(Name = "X-Login")] string requesterLogin,
        CancellationToken token)
    {
        var result = await _userService.GetUsersOlderThanAgeAsync(requesterLogin, request.Age, token);
        return Ok(result);
    }
    
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateUserProfile(
        [FromBody] UpdateUserProfileRequest request,
        [FromHeader(Name = "X-Login")] string requesterLogin,
        CancellationToken token)
    {
        await _userService.UpdateUserProfileAsync(requesterLogin, request, token);
        return Ok();
    }


}