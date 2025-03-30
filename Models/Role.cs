using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("roles")]
public class Role : Model
{
    [Column("name")]
    [MaxLength(400)]
    public required string Name { get; set; }

    [Column("description")]
    [MaxLength(1000)]
    public string? Description { get; set; }

    public List<User> Users { get; set; } = [];

    public List<Permission> Permissions { get; set; } = [];
}