using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public TeamsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllTeams")]
        public IActionResult GetAllTeams()
        {
            var teams = _db.Teams
                .OrderBy(team => team.Profession == "إدارة" ? 0 :
                                  team.Profession == "رجل مبيعات" ? 1 : 2) 
                .ThenBy(team => team.TeamId) 
                .ToList();

            return Ok(teams);
        }





        [HttpGet("GetTeamByID/{id}")]
        public IActionResult TilerID(int id)
        {
            var team = _db.Teams.FirstOrDefault(a => a.TeamId == id); 
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team); 
        }


        [HttpPost("AddTeam")]
        public IActionResult AddTeam([FromForm] TeamDTOs newTeam)
        {
            if (newTeam == null || newTeam.TeamImg == null)
            {
                return BadRequest("Invalid team data or missing image.");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string imageFileName = newTeam.TeamImg.FileName;
            var imageURL = Path.Combine(folderPath, imageFileName);

            using (var stream = new FileStream(imageURL, FileMode.Create))
            {
                newTeam.TeamImg.CopyTo(stream);
            }

            var data = new Team
            {
                TeamName = newTeam.TeamName,
                TeamImg = imageFileName,
                Profession = newTeam.Profession,
                TeamPhoneNum = newTeam.TeamPhoneNum,
            };

            _db.Teams.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }

        [HttpPut("UpdateTeamByID/{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromForm] TeamDTOs teamDTOs)
        {
            var existingTeam = _db.Teams.FirstOrDefault(c => c.TeamId == id);
            if (existingTeam == null)
            {
                return NotFound("Team not found");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string? imageFileName = null;

            if (teamDTOs.TeamImg != null)
            {
                imageFileName = teamDTOs.TeamImg.FileName;
                var imageURL = Path.Combine(folderPath, imageFileName);
                using (var stream = new FileStream(imageURL, FileMode.Create))
                {
                    await teamDTOs.TeamImg.CopyToAsync(stream);
                }
                existingTeam.TeamImg = imageFileName;
            }

            if (!string.IsNullOrEmpty(teamDTOs.TeamName))
                existingTeam.TeamName = teamDTOs.TeamName;

            if (!string.IsNullOrEmpty(teamDTOs.Profession))
                existingTeam.Profession = teamDTOs.Profession;

            if (!string.IsNullOrEmpty(teamDTOs.TeamPhoneNum))
                existingTeam.TeamPhoneNum = teamDTOs.TeamPhoneNum;

            _db.Teams.Update(existingTeam);
            _db.SaveChanges();

            return Ok(existingTeam);
        }


        [HttpDelete("DeleteTeam/{id}")]
        public IActionResult DeleteTeam(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var tea = _db.Teams.FirstOrDefault(cc => cc.TeamId == id);

            if (tea == null)
            {
                return NotFound();
            }
            _db.Teams.Remove(tea);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
