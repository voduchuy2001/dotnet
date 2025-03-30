using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

[Table("permissions")]
public class Permission : Model
{
    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    [MaxLength(400)]
    public string? Description { get; set; }

    public List<Role> Roles { get; set; } = [];
}