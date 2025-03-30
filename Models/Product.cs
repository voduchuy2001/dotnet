using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

[Table("products")]
public class Product: Model
{
    [Column("name")]
    [MaxLength(400)]
    public required string Name { get; set; }
    
    [Column("thumb")]
    [MaxLength(400)]
    public string? Thumb { get; set; }
    
    [Column("images")]
    [MaxLength(1000)]
    public string? Images { get; set; }
    
    [Column("stock")]
    public int? Stock { get; set; }
    
    [Column("description")]
    [MaxLength(1000000000)]
    public string? Description { get; set; }
    
    [Column("price")]
    [Precision(12, 2)]
    public decimal Price { get; set; }
}