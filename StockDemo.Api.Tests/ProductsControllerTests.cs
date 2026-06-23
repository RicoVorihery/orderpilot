using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using StockDemo.Api.Models;

namespace StockDemo.Api.Tests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("x-api-key", "sk_orderpilot_dev_a1b2c3d4");
        }

        [Fact]
        public async Task GetProducts_CallGetProductsAsync_ReturnProducts()
        {
            //Act
            var response = await _client.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadFromJsonAsync<List<Product>>();

            //Assert
            Assert.NotNull(payload);
            Assert.NotEmpty(payload);
        }
    }
}