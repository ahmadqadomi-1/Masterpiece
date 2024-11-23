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

        [HttpGet("SearchProducts")]
        public IActionResult SearchProducts([FromQuery] string name, [FromQuery] decimal? price, [FromQuery] int? categoryId)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProductName.Contains(name));
            }

            if (price.HasValue)
            {
                query = query.Where(p => p.Price == price.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = query.ToList();
            return Ok(products);
        }

        [HttpPost("AddNewProduct")]
        public IActionResult AddProduct([FromForm] ProductRequest req)
        {
            var data = new Product
            {
                ProductName = req.ProductName,
                ProductDescription = req.ProductDescription,
                Price = req.Price,
                Stock = req.Stock,
                ProductRate = req.ProductRate,
                CategoryId = req.CategoryId,
                ProductImage = req.ProductImage
            };
            _db.Products.Add(data);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateTheProductByID/{id}")]
        public IActionResult UpdateProduct(int id, [FromForm] ProductRequest request)
        {
            var upproduct = _db.Products.FirstOrDefault(t => t.ProductId == id);

            upproduct.ProductName = request.ProductName;
            upproduct.ProductDescription = request.ProductDescription;
            upproduct.Price = request.Price;
            upproduct.Stock = request.Stock;
            upproduct.ProductRate = request.ProductRate;
            upproduct.ProductImage = request.ProductImage;
            upproduct.CategoryId = request.CategoryId;

            _db.Products.Update(upproduct);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cate = _db.Products.FirstOrDefault(cc => cc.ProductId == id);

            if (cate == null)
            {
                return NotFound();
            }
            _db.Products.Remove(cate);
            _db.SaveChanges();
            return NoContent();
        }


        [HttpGet("checkrating")]
        public IActionResult checkrating()
        {
            var count = _db.Ratings.Count();
            var x = _db.Ratings.Sum(x => x.RatingValue) / count;
            return Ok(x);
        }


        [HttpGet("GetProductRate/{id}")]
        public IActionResult GetProductRate(int id)
        {
            var product = _db.Products
                .Where(p => p.ProductId == id)
                .Select(p => new { p.ProductId, p.ProductRate })
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

    }
}
