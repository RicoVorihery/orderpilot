using Microsoft.AspNetCore.Mvc;
using StockDemo.Api.Models;
using StockDemo.Api.Repositories;

namespace StockDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? search)
    {
        var products = await _productRepository.GetProductsAsync(search);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if(product ==null) return NotFound();
        return Ok(product);
    }
}