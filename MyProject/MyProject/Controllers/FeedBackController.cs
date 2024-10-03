using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly MyDbContext _db;
        public FeedBackController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllFeedback")]
        public IActionResult Ti()
        {
            var PP = _db.Feedbacks.ToList();
            return Ok(PP);
        }



        [HttpGet("GetFeedbackByID/{id}")]
        public IActionResult FeedID(int id)
        {
            var Ta = _db.Feedbacks.Find(id);
            return Ok(Ta);
        }


        //[HttpPost("AddFeedback")]
        //public IActionResult AddFeed([FromForm] UserRequestDTO user)
        //{

        //    var dataResponse = new User
        //    {
        //        Username = user.Username,
        //        Password = user.Password,
        //        Email = user.Email
        //    };

        //    _db.Users.Add(dataResponse);
        //    _db.SaveChanges();
        //    return Ok(user);

        //}
    }
}
