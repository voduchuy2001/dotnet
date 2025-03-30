using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("permissions")]
public class Permission: Model
{
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    public List<Role> Roles{ get; set; } =  [];
}