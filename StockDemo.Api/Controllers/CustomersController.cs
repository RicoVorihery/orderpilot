using Microsoft.AspNetCore.Mvc;
using StockDemo.Api.Models;
using StockDemo.Api.Repositories;

namespace StockDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController:ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;            
        }
        
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if(customer==null) return NotFound();

            return Ok(customer);
        }

    }
}