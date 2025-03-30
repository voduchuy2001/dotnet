using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.Models;

[Table("users")]
public class User : Model
{
    [Column("name")]
    [MaxLength(400)]
    public required string Name { get; set; }

    [Column("email")]
    [MaxLength(400)]
    public required string Email { get; set; }

    [Column("password")]
    [JsonIgnore]
    [MaxLength(400)]
    public required string Password { get; set; }

    [Column("email_verified_at")]
    public DateTime? EmailVerifiedAt { get; set; }

    public List<Role> Roles { get; set; } = [];
}