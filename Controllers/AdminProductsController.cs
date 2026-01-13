using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProteinStore.API.Data;
using ProteinStore.API.Models;

namespace ProteinStore.API.Controllers
{
    [ApiController]
    [Route("api/admin/products")]
    [Authorize(Roles = "Admin")]
    public class AdminProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product name is required");

            if (string.IsNullOrWhiteSpace(product.ImageUrl))
                return BadRequest("ImageUrl is required");

            // ✅ allow external OR local images
            if (!product.ImageUrl.StartsWith("http") &&
                !product.ImageUrl.StartsWith("/products"))
            {
                return BadRequest("ImageUrl must be external or start with /products");
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}