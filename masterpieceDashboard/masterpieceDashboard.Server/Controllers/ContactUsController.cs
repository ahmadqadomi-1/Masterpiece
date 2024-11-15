using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ContactUsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllContacts")]
        public IActionResult GetAllContacts()
        {
            var Con = _db.ContactUs.ToList();
            return Ok(Con);
        }

        [HttpGet("GetByDesc")]
        public IActionResult GetContact()
        {
            var contacts = _db.ContactUs
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            var formattedContacts = contacts.Select(c => new
            {
                c.Message,
                c.Id,
                c.Name,
                c.PhoneNumber,
                c.Email,
                CreatedAt = c.CreatedAt.HasValue
                    ? c.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm")
                    : "N/A"
            }).ToList();

            return Ok(formattedContacts);
        }

        [HttpPost("AddContact")]
        public IActionResult AddMessage([FromForm] ContactRequest request)
        {
            var newContact = new ContactU
            {
                Name = request.Name,
                Email = request.Email,
                Message = request.Message,
                PhoneNumber = request.PhoneNumber,
            };
            _db.ContactUs.Add(newContact);
            _db.SaveChanges();
            return Ok(new { message = "Contact added successfully" });
        }

        [HttpDelete("DeleteContact/{id}")]
        public IActionResult DeleteContact(int id)
        {
            if (id <= 0)
            {
                return BadRequest("No Contact For This ID");
            }

            var cintact = _db.ContactUs.FirstOrDefault(u => u.Id == id);
            if (cintact == null)
            {
                return NotFound();
            }
            _db.ContactUs.Remove(cintact);
            _db.SaveChanges();
            return Ok();
        }
    }
}
