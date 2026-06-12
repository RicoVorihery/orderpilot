namespace StockDemo.Api.Models;

public class Stock
{
    public int Id {get;set;}
    public int ProductId {get;set;}
    public int WarehouseId {get;set;}
    public int Quantity {get;set;}
    public DateTime Created_at {get;set;}
    public DateTime Updated_at {get;set;}
}
