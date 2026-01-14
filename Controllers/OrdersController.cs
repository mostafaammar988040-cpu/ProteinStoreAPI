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
            // 🔒 VALIDATION
            if (dto.Items == null || dto.Items.Count == 0)
                return BadRequest("Order must contain at least one item.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required.");

            var order = new Order
            {
                CustomerName = dto.CustomerName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                OrderDate = DateTime.UtcNow, // ✅ FIXED
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            foreach (var item in dto.Items)
            {
                if (item.Quantity <= 0)
                    return BadRequest("Invalid quantity.");

                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    return BadRequest($"Product {item.ProductId} not found.");

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                total += product.Price * item.Quantity;
            }

            order.TotalPrice = total;

            // ✅ SAVE ORDER (ONCE)
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

            // ✅ SEND EMAILS (NON-BLOCKING)
            try
            {
                _emailService.SendOrderConfirmation(order.Email, order.Id, order.TotalPrice);
                _emailService.SendManagerOrderNotification(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EMAIL FAILED: " + ex.Message);
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
    }
}