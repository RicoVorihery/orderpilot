namespace StockDemo.Api.Models;

public class Customer
{
    public int Id {get;set;}
    public string Lastname {get;set;}
    public string Firstname {get;set;}
    public string Address {get;set;}
    public string City {get;set;}
    public string Province {get;set;}
    public string PostalCode {get;set;}
    public string Email {get;set;}
    public string Phone {get;set;}
    public DateTime Created_at {get;set;}
    public DateTime Updated_at {get;set;}
}
