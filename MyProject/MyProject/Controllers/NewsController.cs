using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
using MyProject.Models;

namespace MyProject.Controllers
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
        [HttpPost("AddNewNews")]
        public IActionResult AddNews([FromForm] DTOsNews news)
        {
            var NewNews = new News
            {
                NewsName = news.NewsName,
                YoutubeUrl = news.YoutubeUrl,
                NewsDescription = news.NewsDescription,
                NewsDate = news.NewsDate,
            };
            _db.News.Add(NewNews);
            _db.SaveChanges();
            return Ok(NewNews);
        }

        [HttpGet("GetLatestNews")]
        public IActionResult GetLatestNews()
        {
            var latestNews = _db.News
                .OrderByDescending(p => p.NewsDate)
                .Take(3)
                .ToList();

            return Ok(latestNews);
        }

    }
}
