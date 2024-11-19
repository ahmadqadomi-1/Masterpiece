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
        public IActionResult Ti()
        {
            var PP = _db.Teams
                .OrderBy(team => team.Profession == "إدارة" ? 0 :
                                  team.Profession == "رجل مبيعات" ? 2 : 1)
                .ThenBy(team => team.TeamName) 
                .ToList();
            return Ok(PP);
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
        public IActionResult AddTeam([FromBody] TeamDTOs newTeam)
        {
            var data = new Team
            {
                TeamName = newTeam.TeamName,
                TeamImg = newTeam.TeamImg,
                Profession = newTeam.Profession,
                TeamPhoneNum = newTeam.TeamPhoneNum,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Teams.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] TeamDTOs teamDTOs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTeam = _db.Teams.FirstOrDefault(c => c.TeamId == id);

            if (existingTeam == null)
            {
                return NotFound();
            }
            existingTeam.TeamName = teamDTOs.TeamName;
            existingTeam.TeamImg = teamDTOs.TeamImg;
            existingTeam.Profession = teamDTOs.Profession;
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
