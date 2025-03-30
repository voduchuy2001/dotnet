using Api.Dtos;
using Api.Requests;

namespace Api.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequest request);
    Task<string> Register(RegisterRequest request);
    Task<AuthUserDto?> Authenticated(string email);
}