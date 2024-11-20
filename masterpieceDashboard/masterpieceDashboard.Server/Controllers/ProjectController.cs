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
        public IActionResult AddProject([FromForm] DTOsProject req)
        {
            if (req == null)
            {
                return BadRequest("Invalid project data.");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string imageFileName = req.ProjectImage?.FileName ?? string.Empty;

            if (!string.IsNullOrEmpty(imageFileName))
            {
                var imageURL = Path.Combine(folderPath, imageFileName);
                if (!System.IO.File.Exists(imageURL))
                {
                    using (var stream = new FileStream(imageURL, FileMode.Create))
                    {
                        req.ProjectImage.CopyTo(stream);
                    }
                }
            }

            var newProject = new Project
            {
                ProjectName = req.ProjectName,
                ProjectType = req.ProjectType,
                ProjectDate = req.ProjectDate,
                Location = req.Location,
                Description = req.Description,
                ProjectImage = imageFileName
            };

            _db.Projects.Add(newProject);
            _db.SaveChanges();
            return Ok(newProject);
        }


        [HttpPut("UpdateProjectByID/{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromForm] DTOsProject Proo)
        {
            var upProject = _db.Projects.FirstOrDefault(t => t.ProjectId == id);
            if (upProject == null)
            {
                return NotFound("Project not found");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string? imageFileName = null;

            if (Proo.ProjectImage != null)
            {
                imageFileName = Proo.ProjectImage.FileName;
                var imageURL = Path.Combine(folderPath, imageFileName);
                using (var stream = new FileStream(imageURL, FileMode.Create))
                {
                    await Proo.ProjectImage.CopyToAsync(stream);
                }
            }

            if (!string.IsNullOrEmpty(Proo.ProjectName))
                upProject.ProjectName = Proo.ProjectName;

            if (!string.IsNullOrEmpty(Proo.ProjectType))
                upProject.ProjectType = Proo.ProjectType;

            if (Proo.ProjectDate.HasValue)
                upProject.ProjectDate = Proo.ProjectDate.Value;

            if (!string.IsNullOrEmpty(Proo.Location))
                upProject.Location = Proo.Location;

            if (!string.IsNullOrEmpty(Proo.Description))
                upProject.Description = Proo.Description;

            if (!string.IsNullOrEmpty(imageFileName))
                upProject.ProjectImage = imageFileName;

            _db.Projects.Update(upProject);
            _db.SaveChanges();
            return Ok(upProject);
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
