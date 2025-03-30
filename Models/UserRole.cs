using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("user_role")]
public class UserRole: Model
{
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("role_id")]
    public int RoleId { get; set; }
    
    public User User { get; set; } = null!;
    
    public Role Role { get; set; } = null!;
}