namespace masterpieceDashboard.Server.DTOs
{
    public class ProductRequest
    {
        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductDescriptionList1 { get; set; }

        public string? ProductDescriptionList2 { get; set; }

        public string? ProductDescriptionList3 { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public decimal? ProductRate { get; set; }

        public IFormFile? ProductImage { get; set; }

        public IFormFile? ProductImage2 { get; set; }

        public IFormFile? ProductImage3 { get; set; }

        public int? CategoryId { get; set; }
    }
}
