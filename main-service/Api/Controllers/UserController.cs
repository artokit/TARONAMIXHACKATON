using Api.Controllers.Abstractions;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Route("api/users")]
public class UserController : BaseController
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetUserMe()
    {
        return Ok(await _userService.GetMe(UserId));
    }
}