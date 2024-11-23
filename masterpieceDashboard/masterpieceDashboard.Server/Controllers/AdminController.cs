using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MyDbContext _db;

        public AdminController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllAdmins")]
        public IActionResult Cat()
        {
            var CC = _db.Admins.ToList();
            return Ok(CC);
        }

        [HttpPost("login")]
        public IActionResult Log([FromForm] UserAdminDTO admin)
        {
            var logg = _db.Admins.FirstOrDefault(x => x.Email == admin.Email && x.Password == admin.Password);
            if (logg == null)
            {
                return BadRequest(new { message = "Password or email invalid" });
            }

            return Ok(new { userId = logg.Id, userName = logg.Username });
        }


    }
}
