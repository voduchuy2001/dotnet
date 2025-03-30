using System.Text.Json.Serialization;

namespace Api.Requests;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}