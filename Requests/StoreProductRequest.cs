using System.Text.Json.Serialization;

namespace Api.Requests;

public class StoreProductRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("stock")]
    public int Stock { get; set; }
    
    [JsonPropertyName("thumb")]
    public string? Thumb { get; set; }
    
    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = [];
}