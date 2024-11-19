namespace masterpieceDashboard.Server.DTOs
{
    public class OrderProductDetailsDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price; // السعر الإجمالي
    }
}
