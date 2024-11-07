using masterpieceDashboard.Server.DTOs;
using masterpieceDashboard.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace masterpieceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProjectController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllProject")]
        public IActionResult Ti()
        {
            var PP = _db.Projects.ToList();
            return Ok(PP);
        }

        [HttpGet("GetProjectByID/{id}")]
        public IActionResult ProjectID(int id)
        {
            var Ta = _db.Projects.Find(id);
            return Ok(Ta);
        }

        [HttpGet("GetLatestProjects")]
        public IActionResult GetLatestProjects()
        {
            var latestProjects = _db.Projects
                .OrderByDescending(p => p.ProjectDate)
                .Take(3)
                .ToList();

            return Ok(latestProjects);
        }

        [HttpPost("AddProject")]
        public IActionResult AddProject([FromBody] Project newProject)
        {
            if (newProject == null)
            {
                return BadRequest("Invalid project data.");
            }
            _db.Projects.Add(newProject);
            _db.SaveChanges();
            return Ok(newProject);
        }

        [HttpPut("UpdateProjectByID/{id}")]
        public IActionResult UpdateProject(int id, [FromForm] DTOsProject Proo)
        {
            var upProject = _db.Projects.FirstOrDefault(t => t.ProjectId == id);
            upProject.ProjectName = Proo.ProjectName;
            upProject.ProjectType = Proo.ProjectType;
            upProject.ProjectImage = Proo.ProjectImage;

            _db.Projects.Update(upProject);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeleteProject/{id}")]
        public IActionResult DeleteProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cate = _db.Projects.FirstOrDefault(cc => cc.ProjectId == id);

            if (cate == null)
            {
                return NotFound();
            }
            _db.Projects.Remove(cate);
            _db.SaveChanges();
            return Ok(cate);
        }
    }
}
