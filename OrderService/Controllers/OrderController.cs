using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderRepository;

        public OrdersController(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderRepository.GetAllOrdersAsync());
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        //[HttpGet("GetOrdersByUser/{userId}")]
        //public async Task<IActionResult> GetOrdersByUser(long userId)
        //{
        //    var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        //    return Ok(orders);
        //}

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(long id, Order order)
        {
            if (id != order.OrderId) return BadRequest();
            await _orderRepository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
