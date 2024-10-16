using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly MyDbContext _db;
        public CartItemController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("cartitem")]
        public IActionResult getCartItems()
        {
            var cartItem = _db.CartItems.Select(x => new cartItemResponseDTO
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,
                Quantity = x.Quantity,
                Product = new ProductMainRequest
                {
                    ProductName = x.Product.ProductName,
                    Price = x.Product.Price,
                    ProductImage = x.Product.ProductImage,
                }
            });
            return Ok(cartItem);
        }

        [HttpPost("addtocart")]
        public IActionResult AddCartItem([FromBody] CartItemRequest cart)
        {
            // Step 1: Retrieve the available stock of the product from the database
            var product = _db.Products.SingleOrDefault(p => p.ProductId == cart.ProductId);
            if (product == null)
            {
                return NotFound("The product does not exist.");
            }

            // Step 2: Check if the requested quantity is available
            if (product.Stock < cart.Quantity)
            {
                return BadRequest("The requested quantity is not available.");
            }

            // Step 3: Retrieve or create the user's cart
            var userCart = _db.Carts.SingleOrDefault(c => c.UserId == cart.UserId);
            if (userCart == null)
            {
                userCart = new Cart { UserId = cart.UserId };
                _db.Carts.Add(userCart);
                _db.SaveChanges(); // Save the new cart to the database
            }

            // Step 4: Create a new cart item
            var cartItem = new CartItem
            {
                CartId = userCart.CartId,
                Quantity = cart.Quantity,
                ProductId = cart.ProductId,
                UserId = cart.UserId,
            };

            // Add the cart item to the database
            _db.CartItems.Add(cartItem);

            // Update the product's stock in the database
            product.Stock -= cart.Quantity;

            // Save the changes to the database
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("update/updateCart/{id}")]
        public IActionResult updateCart(int id, [FromBody] CartItemRequest cart)
        {
            var cartItem = _db.CartItems.FirstOrDefault(ci => ci.CartItemId == id);

            if (cartItem == null)
            {
                return BadRequest();
            }
            cartItem.Quantity = cart.Quantity;
            var updatecItems = _db.CartItems.Update(cartItem);
            _db.SaveChanges();
            if (updatecItems == null)
            {
                return BadRequest();
            }
            return Ok(updatecItems);
        }



        [HttpDelete("DeleteCartItem")]
        public IActionResult deleteCart()
        {
            var Carts = _db.CartItems.ToList();
            foreach (var cart in Carts) {

                _db.CartItems.Remove(cart);
            }

            _db.SaveChanges();
            return Ok();
        }


        [HttpDelete("delete/deleteItem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cartItem = _db.CartItems.FirstOrDefault(ci => ci.CartItemId == id);

            if (cartItem == null)
            {
                return NotFound();
            }
            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpGet("cart/GetCartItemsForUser/{userID}")]
        public IActionResult GetCartItemsForUser(int userID)
        {
            var user = _db.Carts.FirstOrDefault(x => x.UserId == userID);
            if (user == null)
            {
                return NotFound("User cart not found.");
            }

            var cartItem = _db.CartItems.Where(ci => ci.CartId == user.CartId).Select(
                x => new cartItemResponseDTO
                {
                    CartItemId = x.CartItemId,
                    CartId = x.CartId,
                    Quantity = x.Quantity,
                    Product = new ProductMainRequest
                    {
                        ProductId = x.Product.ProductId,
                        ProductName = x.Product.ProductName,
                        Price = x.Product.Price,
                    }
                }).ToList();
            return Ok(cartItem);
        }

        [HttpGet("cartitem/{id}")]
        public IActionResult getCartItems(int id)
        {
            var cartItem = _db.CartItems.Where(c => c.ProductId == id).ToList();
            return Ok(cartItem);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetCartItems/{userID}")]
        public IActionResult GetCartItems(int userID)
        {
            var cartItems = _db.CartItems.Select(p => new
            {
                p.Quantity,
                p.CartId,
                p.ProductId,
                p.CartItemId,
                Cart = new
                {
                    p.Cart.UserId,
                },
                Product = new
                {
                    p.Product.ProductImage,
                    p.Product.ProductName,
                    p.Product.Price,
                    p.Product.Category.CategoryName,
                }
            }).Where(p => p.Cart.UserId == userID).ToList();
            return Ok(cartItems);
        }
        ////////////////////////////////////
        [HttpPost("AddCartItem/{UserId}")]
        public IActionResult AddCartItem([FromBody] CartItemRequest newItem, int UserId)
        {
            // Check if the user has a cart
            var user = _db.Carts.FirstOrDefault(x => x.UserId == UserId);

            if (user == null)
            {
                return NotFound("Cart not found for this user.");
            }

            // Check if the product is already in the user's cart
            var checkSelectedProduct = _db.CartItems.FirstOrDefault(x => x.ProductId == newItem.ProductId && x.CartItemId == user.CartId);

            if (checkSelectedProduct == null)
            {
                // Add new product to cart
                var data = new CartItem
                {

                    CartItemId = user.CartId,
                    ProductId = newItem.ProductId,
                    Quantity = newItem.Quantity,
                };

                _db.CartItems.Add(data);
                _db.SaveChanges();
                return Ok("Product added to cart");
            }
            else
            {
                // Update the quantity of the existing product in the cart
                checkSelectedProduct.Quantity += newItem.Quantity;

                _db.CartItems.Update(checkSelectedProduct);
                _db.SaveChanges();
                return Ok("Quantity of product increased");
            }
        }

        [HttpGet("getUserCartItems/{UserId}")]
        public IActionResult getUserCartItems(int UserId)
        {

            var user = _db.Carts.FirstOrDefault(x => x.UserId == UserId);

            var cartItem = _db.CartItems.Where(c => c.CartItemId == user.CartId).Select(
             x => new cartItemResponseDTO
             {
                 CartItemId = x.CartItemId,
                 CartId = x.CartItemId,
                 Product = new ProductMainRequest
                 {
                     ProductId = x.Product.ProductId,
                     ProductName = x.Product.ProductName,
                     Price = x.Product.Price,
                     ProductImage = x.Product.ProductImage,
                 },
                 Quantity = x.Quantity,
             });



            return Ok(cartItem);
        }
    }
}
