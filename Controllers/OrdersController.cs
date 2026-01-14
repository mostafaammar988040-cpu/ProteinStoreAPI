using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProteinStore.API.Data;
using ProteinStore.API.DTOs;
using ProteinStore.API.Models;
using ProteinStore.API.Services;

namespace ProteinStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public OrdersController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // ✅ CREATE ORDER (PUBLIC)
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDto dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
                return BadRequest("Order must contain at least one item.");

            var order = new Order
            {
                CustomerName = dto.CustomerName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            foreach (var item in dto.Items)
            {
                if (item.Quantity <= 0)
                    return BadRequest("Invalid quantity");

                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    return BadRequest($"Product {item.ProductId} not found");

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                total += product.Price * item.Quantity;
                order.OrderItems.Add(orderItem);
            }

            order.TotalPrice = total;
try
{
    _context.Orders.Add(order);
    _context.SaveChanges();
}
catch (Exception ex)
{
    return StatusCode(500, new
    {
        error = ex.Message,
        inner = ex.InnerException?.Message
    });
}

            _context.SaveChanges();


            try
            {
                _emailService.SendOrderConfirmation(order.Email, order.Id, order.TotalPrice);
                _emailService.SendManagerOrderNotification(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EMAIL FAILED: " + ex.ToString());
                Console.WriteLine("Email error: " + ex.Message);
            }
            return Ok(new
            {
                message = "Order placed successfully",
                orderId = order.Id
            });
        }

        // 🔐 ADMIN ONLY
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return Ok(orders);
        }

        // 🔐 ADMIN ONLY
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public IActionResult UpdateOrderStatus(int id, [FromBody] string status)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            order.Status = status;
            _context.SaveChanges();

            return Ok();
        }
    }
}