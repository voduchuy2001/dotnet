using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("role_permission")]
public class RolePermission: Model
{
    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("permission_id")]
    public int PermissionId { get; set; }
    
    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;
}