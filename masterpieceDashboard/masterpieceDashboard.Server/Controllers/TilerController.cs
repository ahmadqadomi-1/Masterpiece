using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace masterpieceDashboard.Server.Controllers
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
            var Ta = _db.Tilers.FirstOrDefault(a => a.TilerId == id);
            if (Ta == null)
            {
                return NotFound();
            }
            return Ok(Ta);
        }

        [HttpPost("AddTiler")]
        public IActionResult AddTiler([FromForm] TilerDTOs tilerDT)
        {

            if (tilerDT == null || tilerDT.TilerImg == null)
            {
                return BadRequest("Invalid team data or missing image.");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string imageFileName = tilerDT.TilerImg.FileName;
            var imageURL = Path.Combine(folderPath, imageFileName);

            using (var stream = new FileStream(imageURL, FileMode.Create))
            {
                tilerDT.TilerImg.CopyTo(stream);
            }

            var data = new Tiler
            {
                TilerName = tilerDT.TilerName,
                TilerImg = imageFileName,
                Profession = tilerDT.Profession,
                TilerPhoneNum = tilerDT.TilerPhoneNum,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Tilers.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }

        [HttpPut("UpdateTilerByID/{id}")]
        public async Task<IActionResult> UpdateTiler(int id, [FromForm] TilerDTOs tilerDTOs)
        {
            var existingTiler = _db.Tilers.FirstOrDefault(c => c.TilerId == id);
            if (existingTiler == null)
            {
                return NotFound("Tiler not found");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string? imageFileName = null;

            if (tilerDTOs.TilerImg != null)
            {
                imageFileName = tilerDTOs.TilerImg.FileName;
                var imageURL = Path.Combine(folderPath, imageFileName);
                using (var stream = new FileStream(imageURL, FileMode.Create))
                {
                    await tilerDTOs.TilerImg.CopyToAsync(stream);
                }
                existingTiler.TilerImg = imageFileName;
            }

            if (!string.IsNullOrEmpty(tilerDTOs.TilerName))
                existingTiler.TilerName = tilerDTOs.TilerName;

            if (!string.IsNullOrEmpty(tilerDTOs.Profession))
                existingTiler.Profession = tilerDTOs.Profession;

            if (!string.IsNullOrEmpty(tilerDTOs.TilerPhoneNum))
                existingTiler.TilerPhoneNum = tilerDTOs.TilerPhoneNum;

            _db.Tilers.Update(existingTiler);
            await _db.SaveChangesAsync();

            return Ok(existingTiler);
        }

        [HttpDelete("DeleteTiler/{id}")]
        public IActionResult DeleteTiler(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var tea = _db.Tilers.FirstOrDefault(cc => cc.TilerId == id);

            if (tea == null)
            {
                return NotFound();
            }
            _db.Tilers.Remove(tea);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
