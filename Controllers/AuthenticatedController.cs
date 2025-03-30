using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("auth")]
[ApiController]
public class AuthenticatedController(IAuthService authService): ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpGet("me")]
    public async Task<IActionResult> Authenticated()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var user = await _authService.Authenticated(token);
        return Ok(user);
    }
}