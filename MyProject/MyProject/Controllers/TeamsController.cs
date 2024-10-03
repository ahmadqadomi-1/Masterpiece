using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
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
            var PP = _db.Teams.ToList();
            return Ok(PP);
        }



        [HttpGet("GetTeamByID/{id}")]
        public IActionResult TilerID(int id)
        {
            var Ta = _db.Teams.Where(a => a.TeamId== id).ToList();
            return Ok(Ta);
        }
    }
}
