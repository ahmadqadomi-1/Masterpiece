namespace MyProject.DTOs
{
    public class CartItemRequest
    {
        public int? UserId { get; set; }


        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}
