using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TilerController : ControllerBase
    {
        private readonly MyDbContext _db;
        public TilerController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllTilers")]
        public IActionResult Ti()
        {
            var PP = _db.Tilers.ToList();
            return Ok(PP);
        }



        [HttpGet("GetTilerByID/{id}")]
        public IActionResult TilerID(int id)
        {
            var Ta = _db.Tilers.Where(a => a.TilerId == id).ToList();
            return Ok(Ta);
        }
    }
}
