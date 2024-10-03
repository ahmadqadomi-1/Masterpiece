using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DTOs;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult Pro()
        {
            var PP = _db.Products.ToList();
            return Ok(PP);
        }

        [HttpGet("GetAllProductsForOneCategory/{id}")]
        public IActionResult Pr(int id)
        {
            var Ta = _db.Products.Where(a => a.CategoryId == id).ToList();
            return Ok(Ta);
        }

        [HttpGet("GetProductByID/{id}")]
        public IActionResult ProdId(int id)
        {
            var Ta = _db.Products.Find(id);
            return Ok(Ta);
        }

        [HttpGet("GetRelatedProducts/{id}")]
        public IActionResult GetRelatedProduct(int id)
        {
            var product = _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var relatedProducts = _db.Products
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.ProductDescription,
                    p.Price,
                    p.Stock,
                    p.ProductRate
                })
                .ToList();
            return Ok(relatedProducts);  
        }

        [HttpPost("AddQuantityToProductByID/{id}")]

        public IActionResult AddQuan(int id, [FromForm] QuantityDTOs quantityUdateDTO)
        {
            if (quantityUdateDTO.Quantity <= 0)
            {
                return BadRequest("Invalid quantity.");
            }
            var cartItems = new CartItem
            {
                Quantity = quantityUdateDTO.Quantity,
                ProductId = id
            };
            _db.CartItems.Add(cartItems);
            _db.SaveChanges();
            return Ok();
        }
    }
}
