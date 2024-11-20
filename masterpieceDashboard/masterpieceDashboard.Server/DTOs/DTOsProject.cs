namespace masterpieceDashboard.Server.DTOs
{
    public class DTOsProject
    {
        public string? ProjectName { get; set; }

        public string? ProjectType { get; set; }

        public DateTime? ProjectDate { get; set; }

        public IFormFile? ProjectImage { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }
    }
}
