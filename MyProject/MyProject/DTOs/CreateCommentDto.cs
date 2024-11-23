namespace MyProject.DTOs
{
    public class CreateCommentDto
    {
        public string? CommentText { get; set; }
        public int Rating { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }

    }
}
