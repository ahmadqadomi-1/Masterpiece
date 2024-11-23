using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public NewsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllNews")]
        public IActionResult New()
        {
            var PP = _db.News.ToList();
            return Ok(PP);
        }

        [HttpGet("{id}")]
        public IActionResult GetNews(int id)
        {
            var news = _db.News.FirstOrDefault(n => n.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }


        [HttpPost("AddNewNews")]
        public IActionResult AddNews([FromForm] DTOsNews news)
        {
            Console.WriteLine($"Received News: {news.NewsName}, {news.YoutubeUrl}, {news.NewsDescription}");

            var NewNews = new News
            {
                NewsName = news.NewsName,
                YoutubeUrl = news.YoutubeUrl,
                NewsDescription = news.NewsDescription,
                NewsDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _db.News.Add(NewNews);
            _db.SaveChanges();
            return Ok(NewNews);
        }




        [HttpPut("{id}")]
        public IActionResult UpdateNews(int id, [FromForm] DTOsNews news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingNews= _db.News.FirstOrDefault(c => c.NewsId == id);

            if (existingNews == null)
            {
                return NotFound();
            }
            existingNews.YoutubeUrl = news.YoutubeUrl;
            existingNews.NewsDescription = news.NewsDescription;
            existingNews.NewsName = news.NewsName;

            _db.News.Update(existingNews);
            _db.SaveChanges();
            return Ok(existingNews);
        }

        [HttpDelete("DeleteNews/{id}")]
        public IActionResult DeleteNews(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var tea = _db.News.FirstOrDefault(cc => cc.NewsId == id);

            if (tea == null)
            {
                return NotFound();
            }
            _db.News.Remove(tea);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
