using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
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

        [HttpPost("AddTiler")]
        public IActionResult AddTiler([FromBody] TilerDTOs tilerDT)
        {
            var data = new Tiler
            {
                TilerName = tilerDT.TilerName,
                TilerImg = tilerDT.TilerImg,
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

        [HttpPut("{id}")]
        public IActionResult UpdateTiler(int id, [FromBody] TilerDTOs tilerDTOs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTiler = _db.Tilers.FirstOrDefault(c => c.TilerId == id);

            if (existingTiler == null)
            {
                return NotFound();
            }
            existingTiler.TilerName = tilerDTOs.TilerName;
            existingTiler.TilerImg = tilerDTOs.TilerImg;
            existingTiler.Profession = tilerDTOs.Profession;
            existingTiler.TilerPhoneNum = tilerDTOs.TilerPhoneNum;

            _db.Tilers.Update(existingTiler);
            _db.SaveChanges();

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
