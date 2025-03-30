using Api.Requests;
using Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("auth")]
[ApiController]
public class RegisterController(IAuthService authService, IServiceProvider serviceProvider): ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<RegisterRequest>>();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return UnprocessableEntity(result.Errors);
        }
        
        var token = await _authService.Register(request);
        return Ok(token);
    }
}