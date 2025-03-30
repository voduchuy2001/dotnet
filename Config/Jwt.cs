using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Config;

public class Jwt(IConfiguration configuration)
{
    public string SecretKey { get; } = configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("SecretKey is missing");
    public string Issuer { get; } = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("Issuer is missing");
    public string Audience { get; } = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("Audience is missing");
    public int Expiration { get; } = 60;

    public SymmetricSecurityKey SigningKey => new(Encoding.UTF8.GetBytes(SecretKey));

    public TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SigningKey,
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}