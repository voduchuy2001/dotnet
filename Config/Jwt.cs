using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Config;

public class JWT
{
    public string SecretKey { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int Expiration { get; } = 60;

    public SymmetricSecurityKey SigningKey => new(Encoding.UTF8.GetBytes(SecretKey));

    public JWT(IConfiguration configuration)
    {
        SecretKey = configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("SecretKey is missing");
        Issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("Issuer is missing");
        Audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("Audience is missing");
    }

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