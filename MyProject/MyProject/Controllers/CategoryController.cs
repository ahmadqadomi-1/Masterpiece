using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
using MyProject.Models;

namespace MyProject.Controllers
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
            var AA = _db.Categories.Find(id);
            return Ok(AA);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoriesDTO category)
        {
            var data = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Categories.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoriesDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = _db.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryImage = category.CategoryImage;

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
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return NoContent();
        }


        [HttpGet("images/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine("C:\\Users\\Orange\\Desktop\\My Web Sites\\Masterpiece\\masterpieceDashboard\\masterpieceDashboard.Server\\img", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                return PhysicalFile(imagePath, "image/jpeg");
            }
            return NotFound();
        }


    }
}
