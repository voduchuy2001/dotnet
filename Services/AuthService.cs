using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Config;
using Api.Dtos;
using Api.Models;
using Api.Repositories.Interfaces;
using Api.Requests;
using Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AuthService(IAuthRepository authRepository, Jwt jwt) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly Jwt _jwt = jwt;

    public async Task<string> Login(LoginRequest request)
    {
        var user = await _authRepository.FindUserByEmail(request.Email);
        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        
        bool isVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!isVerified)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        
        return GenerateToken(user);
    }
    
    public async Task<string> Register(RegisterRequest request)
    {
        var user = await _authRepository.FindUserByEmail(request.Email);
        if (user is not null)
        {
            throw new UnauthorizedAccessException("Email already exists");
        }

        var hashedPwd = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var registeredUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = hashedPwd
        };
        
        await _authRepository.RegisterUser(registeredUser);
        return GenerateToken(registeredUser);
    }

    public async Task<AuthUserDto?> Authenticated(string token)
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return null;
        }
        var user = await _authRepository.GetAuthenticatedUser(email);
        if (user is null)
        {
            return null;
        }
        var formattedAuthUser = new AuthUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Roles = user.Roles
                .Select(role  => role.Name)
                .ToList(),
            Permissions = user.Roles
                .SelectMany(role => role.Permissions)
                .Select(permission => permission.Name)
                .Distinct()
                .ToList()
        };
        
        return formattedAuthUser;
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_jwt.Expiration),
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            SigningCredentials = new SigningCredentials(_jwt.SigningKey, SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}