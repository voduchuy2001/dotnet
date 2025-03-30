using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

[Table("products")]
public class Product: Model
{
    [Column("name")]
    public string Name { get; set; }
    
    [Column("thumb")]
    public string? Thumb { get; set; }
    
    [Column("images")]
    public string? Images { get; set; }
    
    [Column("stock")]
    public int? Stock { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("price")]
    [Precision(12, 2)]
    public decimal Price { get; set; }
}