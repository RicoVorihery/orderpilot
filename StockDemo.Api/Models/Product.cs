namespace StockDemo.Api.Models;

public class Product
{
    public int Id {get;set;}
    public string Reference {get;set;}
    public string Label {get;set;}
    public string Brand {get;set;}
    public string? Description {get;set;}
    public decimal? Price {get;set;} 
    public DateTime Created_at {get;set;}
    public DateTime Updated_at {get;set;}
}

