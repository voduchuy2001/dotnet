using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("roles")]
public class Role: Model
{
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    public List<User> Users { get; set; } = [];
    
    public List<Permission> Permissions { get; set; } = [];
}