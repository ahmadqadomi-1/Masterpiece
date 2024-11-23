using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
using MyProject.Models;
using System.Linq;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CommentsController(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllComments/{productId}")]
        public IActionResult GetAllComments(int productId)
        {
            var comments = _db.Comments
                .Where(c => c.ProductId == productId )
                .Select(c => new CreateCommentDto
                {
                    CommentId = c.CommentId,
                    CommentText = c.CommentText,
                    Rating = c.Rating >= 0 && c.Rating <= 5 ? c.Rating ?? 0 : 0, // Ensure the rating is between 0 and 5
                    Email = c.Email, // Use null-safe access for related entity
                    UserName = c.UserName, // Use null-safe access for related entity
                    ProductId = c.ProductId ?? 0, // Handle nullable ProductId
                    UserId = c.UserId ?? 0 // Handle nullable UserId
                })
                .ToList();

            if (!comments.Any())
            {
                return NotFound("No comments found for the specified product.");
            }

            return Ok(comments);
        }

        // Get count of comments and concatenated texts
        [HttpGet("GetCommentSummary/{productId}")]
        public IActionResult GetCommentSummary(int productId)
        {
            var comments = _db.Comments
                .Where(c => c.ProductId == productId)
                .ToList();

            if (!comments.Any())
            {
                return NotFound("No comments found for the specified product.");
            }

            var commentCount = comments.Count;
            var allCommentTexts = string.Join(", ", comments.Select(c => c.CommentText));

            return Ok(new
            {
                CommentCount = commentCount,
                AllCommentTexts = allCommentTexts
            });
        }

        // Add a new comment for a product
        [HttpPost("AddComment")]
        public IActionResult AddComment([FromBody] CreateCommentDto createCommentDto)
        {
            try
            {
                if (createCommentDto.ProductId == null || createCommentDto.UserId == null)
                {
                    return BadRequest("ProductId and UserId are required.");
                }

                if (createCommentDto.Rating < 0 || createCommentDto.Rating > 5)
                {
                    return BadRequest("Rating must be between 0 and 5.");
                }

                var newComment = new Comment
                {
                    ProductId = createCommentDto.ProductId.Value,
                    UserId = createCommentDto.UserId.Value,
                    CommentText = createCommentDto.CommentText,
                    Rating = createCommentDto.Rating,
                    CreatedAt = DateTime.Now,
                };

                _db.Comments.Add(newComment);
                _db.SaveChanges();

                return Ok("Comment added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
