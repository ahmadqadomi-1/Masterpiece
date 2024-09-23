using AlQadomy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlQadomy.Controllers
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
        [HttpGet("GetAllProduct")]
        public IActionResult Get()
        {
            var pro = _db.Products.ToList();
            return Ok(pro);
        }
        [HttpGet("GetAllProductById/{id}")]
        public IActionResult product(int id) 
        {
            var prod = _db.Products.Where(p => p.ProductId == id);
            if (prod == null)
                {
                    return BadRequest("No Comments Found");
                }
            return Ok(prod);
        }
        [HttpGet("GetAllProductsForOneCategory/{id}")]
        public IActionResult GetPro(int id)
        {
            var cat = _db.Products.Where(a => a.CategoryId == id).ToList();
            if (cat == null)
                {
                    return BadRequest("No Comments Found");
                }
            return Ok(cat);
        }
    }
}
