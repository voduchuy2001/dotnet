using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.Models;

[Table("users")]
public class User: Model
{
    [Column("name")]
    public string Name { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("password")]
    [JsonIgnore]
    public string Password { get; set; }
    
    [Column("email_verified_at")]
    public DateTime? EmailVerifiedAt { get; set; }

    public List<Role> Roles { get; set; } = [];
}