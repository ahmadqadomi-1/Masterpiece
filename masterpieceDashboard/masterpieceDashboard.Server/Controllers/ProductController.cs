using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace masterpieceDashboard.Server.Controllers
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
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Initialize variables for image file names and URLs
            string image1FileName = req.ProductImage?.FileName ?? string.Empty;
            string image2FileName = req.ProductImage2?.FileName ?? string.Empty;
            string image3FileName = req.ProductImage3?.FileName ?? string.Empty;

            if (!string.IsNullOrEmpty(image1FileName))
            {
                var imageURL1 = Path.Combine(folderPath, image1FileName);
                if (!System.IO.File.Exists(imageURL1))
                {
                    using (var stream = new FileStream(imageURL1, FileMode.Create))
                    {
                        req.ProductImage.CopyTo(stream);
                    }
                }
            }

            if (!string.IsNullOrEmpty(image2FileName))
            {
                var imageURL2 = Path.Combine(folderPath, image2FileName);
                if (!System.IO.File.Exists(imageURL2))
                {
                    using (var stream = new FileStream(imageURL2, FileMode.Create))
                    {
                        req.ProductImage2.CopyTo(stream);
                    }
                }
            }

            if (!string.IsNullOrEmpty(image3FileName))
            {
                var imageURL3 = Path.Combine(folderPath, image3FileName);
                if (!System.IO.File.Exists(imageURL3))
                {
                    using (var stream = new FileStream(imageURL3, FileMode.Create))
                    {
                        req.ProductImage3.CopyTo(stream);
                    }
                }
            }

            var data = new Product
            {
                ProductName = req.ProductName,
                ProductDescription = req.ProductDescription,
                Price = req.Price,
                Stock = req.Stock,
                ProductRate = req.ProductRate,
                CategoryId = req.CategoryId,
                ProductImage = image1FileName,
                ProductImage2 = image2FileName,
                ProductImage3 = image3FileName,
                ProductDescriptionList1 = req.ProductDescriptionList1,
                ProductDescriptionList2 = req.ProductDescriptionList2,
                ProductDescriptionList3 = req.ProductDescriptionList3,
            };

            _db.Products.Add(data);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateTheProductByID/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductRequest request)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string? image1FileName = null;
            string? image2FileName = null;
            string? image3FileName = null;

            // حفظ الصور الجديدة فقط إذا كانت موجودة
            if (request.ProductImage != null)
            {
                image1FileName = request.ProductImage.FileName;
                var imageURL1 = Path.Combine(folderPath, image1FileName);
                using (var stream = new FileStream(imageURL1, FileMode.Create))
                {
                    await request.ProductImage.CopyToAsync(stream);
                }
            }

            if (request.ProductImage2 != null)
            {
                image2FileName = request.ProductImage2.FileName;
                var imageURL2 = Path.Combine(folderPath, image2FileName);
                using (var stream = new FileStream(imageURL2, FileMode.Create))
                {
                    await request.ProductImage2.CopyToAsync(stream);
                }
            }

            if (request.ProductImage3 != null)
            {
                image3FileName = request.ProductImage3.FileName;
                var imageURL3 = Path.Combine(folderPath, image3FileName);
                using (var stream = new FileStream(imageURL3, FileMode.Create))
                {
                    await request.ProductImage3.CopyToAsync(stream);
                }
            }

            var upproduct = _db.Products.FirstOrDefault(t => t.ProductId == id);
            if (upproduct == null)
            {
                return NotFound("Product not found");
            }

            // تحديث الحقول فقط إذا كانت البيانات الجديدة غير فارغة
            if (!string.IsNullOrEmpty(request.ProductName))
                upproduct.ProductName = request.ProductName;

            if (!string.IsNullOrEmpty(request.ProductDescription))
                upproduct.ProductDescription = request.ProductDescription;

            if (!string.IsNullOrEmpty(request.ProductDescriptionList1))
                upproduct.ProductDescriptionList1 = request.ProductDescriptionList1;

            if (!string.IsNullOrEmpty(request.ProductDescriptionList2))
                upproduct.ProductDescriptionList2 = request.ProductDescriptionList2;

            if (!string.IsNullOrEmpty(request.ProductDescriptionList3))
                upproduct.ProductDescriptionList3 = request.ProductDescriptionList3;

            if (request.Price.HasValue)
                upproduct.Price = request.Price.Value;

            if (request.Stock.HasValue)
                upproduct.Stock = request.Stock.Value;

            // تحديث الصور فقط إذا كانت جديدة
            if (!string.IsNullOrEmpty(image1FileName))
                upproduct.ProductImage = image1FileName;

            if (!string.IsNullOrEmpty(image2FileName))
                upproduct.ProductImage2 = image2FileName;

            if (!string.IsNullOrEmpty(image3FileName))
                upproduct.ProductImage3 = image3FileName;

            if (request.CategoryId.HasValue)
                upproduct.CategoryId = request.CategoryId.Value;

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


        [HttpGet("images/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "img", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                return PhysicalFile(imagePath, "image/jpeg");
            }
            return NotFound();
        }


    }
}
