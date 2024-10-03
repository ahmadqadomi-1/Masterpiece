using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
