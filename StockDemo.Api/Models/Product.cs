namespace StockDemo.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Reference { get; set; }
    public string Label { get; set; }
    public string Brand { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

