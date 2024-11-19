namespace masterpieceDashboard.Server.DTOs
{
    public class CreateOrderDTO
    {
        public int? UserId { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? OrderStatus { get; set; }

        public string? Comment { get; set; }
    }
}
