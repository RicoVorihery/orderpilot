using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockDemo.Api.Models;
using StockDemo.Api.Repositories;

namespace StockDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost("drafts")]
        public async Task<ActionResult<Order>> CreateDraftOrder([FromBody] Order order)
        {
            if (order == null) return BadRequest();
            var createdOrder = await _orderRepository.CreateDraftOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }
    }
}