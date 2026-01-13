using Microsoft.AspNetCore.Mvc;
using ProteinStore.API.Data;

namespace ProteinStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET PRODUCTS (PUBLIC)
        [HttpGet]
        public IActionResult GetProducts(
            decimal? minPrice,
            decimal? maxPrice,
            string? categories
        )
        {
            var query = _context.Products.AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(categories))
            {
                var categoryList = categories
                    .Split(',')
                    .Select(c => c.ToLower().Trim())
                    .ToList();

                query = query.Where(p =>
                    categoryList.Contains(p.Category.ToLower())
                );
            }

            return Ok(query.ToList());
        }
    }
}