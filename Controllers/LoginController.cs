using Api.Requests;
using Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("auth")]
[ApiController]
public class LoginController(IAuthService authService, IServiceProvider serviceProvider): ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<LoginRequest>>();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return UnprocessableEntity(result.Errors);
        }
        
        var token = await _authService.Login(request);
        return Ok(token);
    }
}