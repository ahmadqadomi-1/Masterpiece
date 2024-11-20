using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _db;
        public CategoryController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult Cat()
        {
            var CC = _db.Categories.ToList();
            return Ok(CC);
        }

        [HttpGet("GetOneCategoryByID/{id}")]
        public IActionResult Cate(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [HttpPost("AddProductToCategory/{categoryId}")]
        public IActionResult AddProductToCategory(int categoryId, [FromForm] ProductRequest product)
        {
            var category = _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string? image1FileName = null;
            string? image2FileName = null;
            string? image3FileName = null;

            if (product.ProductImage != null)
            {
                image1FileName = product.ProductImage.FileName;
                var imageURL1 = Path.Combine(folderPath, image1FileName);
                using (var stream = new FileStream(imageURL1, FileMode.Create))
                {
                    product.ProductImage.CopyTo(stream);
                }
            }

            if (product.ProductImage2 != null)
            {
                image2FileName = product.ProductImage2.FileName;
                var imageURL2 = Path.Combine(folderPath, image2FileName);
                using (var stream = new FileStream(imageURL2, FileMode.Create))
                {
                    product.ProductImage2.CopyTo(stream);
                }
            }

            if (product.ProductImage3 != null)
            {
                image3FileName = product.ProductImage3.FileName;
                var imageURL3 = Path.Combine(folderPath, image3FileName);
                using (var stream = new FileStream(imageURL3, FileMode.Create))
                {
                    product.ProductImage3.CopyTo(stream);
                }
            }

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductDescriptionList1 = product.ProductDescriptionList1,
                ProductDescriptionList2 = product.ProductDescriptionList2,
                ProductDescriptionList3 = product.ProductDescriptionList3,
                Price = product.Price,
                Stock = product.Stock,
                ProductImage = image1FileName,
                ProductImage2 = image2FileName,
                ProductImage3 = image3FileName,
                CategoryId = categoryId
            };

            _db.Products.Add(newProduct);
            _db.SaveChanges();

            // إعدادات السيريالايزر لمعالجة المراجع الدائرية
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            return new JsonResult(newProduct, options);
        }

        [HttpPost]
        public IActionResult AddCategory([FromForm] CategoriesDTO category)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var imageURL = Path.Combine(folderPath, category.CategoryImage.FileName);
            using (var stream = new FileStream(imageURL, FileMode.Create))
            {
                category.CategoryImage.CopyTo(stream);
            }

            var newCategory = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Categories.Add(newCategory);
            _db.SaveChanges();

            return Ok(newCategory);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromForm] CategoriesDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (category.CategoryImage != null)
            {
                var imageURL = Path.Combine(folderPath, category.CategoryImage.FileName);
                using (var stream = new FileStream(imageURL, FileMode.Create))
                {
                    category.CategoryImage.CopyTo(stream);
                }

                existingCategory.CategoryImage = category.CategoryImage.FileName;
            }

            existingCategory.CategoryName = category.CategoryName;

            _db.Categories.Update(existingCategory);
            _db.SaveChanges();

            return Ok(existingCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID must be greater than 0");
            }

            var category = _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            var productsInCategory = _db.Products.Any(p => p.CategoryId == id);
            if (productsInCategory)
            {
                return BadRequest("Cannot delete this category because it contains products.");
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
