using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockDemo.Api.Repositories;

namespace StockDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController:ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<int>> GetTotalStock(int productId)
        {
            return await _stockRepository.GetTotalStockAsync(productId);
        }

    }
}