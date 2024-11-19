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
            // التحقق من بيانات تسجيل الدخول
            var logg = _db.Admins.FirstOrDefault(x => x.Email == admin.Email && x.Password == admin.Password);
            if (logg == null)
            {
                // إعادة رسالة خطأ واضحة إذا كانت البيانات غير صحيحة
                return BadRequest(new { message = "Password or email invalid" });
            }

            // إعادة الحقول المطلوبة فقط
            return Ok(new { userId = logg.Id, userName = logg.Username });
        }


    }
}
