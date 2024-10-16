using MyProject.Models;

namespace MyProject.DTOs
{
    public class CartItemDTo
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? UserId { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual Product? Product { get; set; }

        public virtual User? User { get; set; }
    }
}
