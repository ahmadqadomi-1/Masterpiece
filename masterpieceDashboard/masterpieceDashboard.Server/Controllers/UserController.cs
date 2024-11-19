using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UserController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUser()
        {
            var data = _db.Users.ToList();
            return Ok(data);
        }

        [HttpGet("GetAllUsersById/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var data = _db.Users.FirstOrDefault(c => c.Email == name);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }



        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            // البحث عن المستخدم مع البيانات المرتبطة
            var user = _db.Users
                .Include(u => u.Orders)
                .Include(u => u.CartItems)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    // حذف الطلبات المرتبطة
                    if (user.Orders?.Any() == true)
                    {
                        _db.Orders.RemoveRange(user.Orders);
                    }

                    // حذف العناصر المرتبطة في العربة
                    if (user.CartItems?.Any() == true)
                    {
                        _db.CartItems.RemoveRange(user.CartItems);
                    }

                    // حذف المستخدم
                    _db.Users.Remove(user);

                    // حفظ التغييرات
                    _db.SaveChanges();

                    // تأكيد الحذف
                    transaction.Commit();

                    // إرجاع الرسالة كـ string
                    return Ok("User has been blocked successfully.");
                }
                catch (Exception ex)
                {
                    // إلغاء التغييرات إذا حدث خطأ
                    transaction.Rollback();
                    return StatusCode(500, $"An error occurred while blocking the user. Details: {ex.Message}");
                }
            }
        }


        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Edituser updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("User data is null.");
            }

            // تنظيف الحقول
            updatedUser.UserName = updatedUser.UserName?.Trim();
            updatedUser.Email = updatedUser.Email?.Trim();
            updatedUser.PhoneNumber = updatedUser.PhoneNumber?.Trim();

            var existingUser = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            if (!string.IsNullOrEmpty(updatedUser.Email) &&
                _db.Users.Any(u => u.Email == updatedUser.Email && u.UserId != id))
            {
                return BadRequest("This email is already in use by another user.");
            }

            existingUser.UserName = updatedUser.UserName ?? existingUser.UserName;
            existingUser.PhoneNumber = updatedUser.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.Email = updatedUser.Email ?? existingUser.Email;

            try
            {
                _db.SaveChanges();
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user. Details: {ex.Message}");
            }
        }


    }
}
