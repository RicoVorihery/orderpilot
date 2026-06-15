namespace StockDemo.Api.Models;

public class Stock
{
    public int Id {get;set;}
    public int ProductId {get;set;}
    public int WarehouseId {get;set;}
    public int Quantity {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
}
