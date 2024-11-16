using Application.Common.Interfaces.Services;
using Contracts.Authorization;
using Contracts.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthorizationController : ControllerBase
{
    private IAuthorizationService _authorizationService;
    
    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto requestDto)
    {
        return Ok(await _authorizationService.Register(requestDto));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        return Ok(await _authorizationService.Login(loginRequestDto));
    }
}