using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DTOs;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _db;
        public OrderController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = _db.Orders
                .Include(o => o.User)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.PaymentMethod,
                    o.OrderStatus,
                    o.OrderDate,
                    o.Comment,
                    UserID = o.User != null ? o.User.UserId : (int?)null, // Corrected syntax for UserID
                    UserName = o.User != null ? o.User.UserName : null,
                    UserEmail = o.User != null ? o.User.Email : null
                })
                .ToList();

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _db.Orders
                .Include(o => o.User)
                .Where(o => o.OrderId == orderId)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.PaymentMethod,
                    o.OrderStatus,
                    o.OrderDate,
                    o.UserId, 
                    o.Comment,
                    UserName = o.User != null ? o.User.UserName : null, 
                    UserEmail = o.User != null ? o.User.Email : null 
                })
                .FirstOrDefault();

            if (order == null)
            {
                return NotFound("لم يتم العثور على الطلب.");
            }

            return Ok(order);
        }

        [HttpGet("GetOrdersByUserId/{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var orders = _db.Orders
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.PaymentMethod,
                    o.OrderStatus,
                    o.OrderDate,
                    o.Comment,
                    UserName = o.User != null ? o.User.UserName : null,
                    UserEmail = o.User != null ? o.User.Email : null
                })
                .ToList();

            if (!orders.Any())
            {
                return NotFound("لم يتم العثور على طلبات لهذا المستخدم.");
            }

            return Ok(orders);
        }


        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderDTO newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = new Order
            {
                UserId = newOrder.UserId,
                TotalAmount = newOrder.TotalAmount,
                PaymentMethod = newOrder.PaymentMethod,
                OrderStatus = newOrder.OrderStatus,
                Comment = newOrder.Comment,
                //OrderDate = newOrder.OrderDate
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            return Ok(order);
        }

        [HttpPut("SetPaymentMethod/{orderId}")]
        public async Task<IActionResult> SetPaymentMethod(int orderId, [FromBody] string paymentMethod)
        {
            
            if (paymentMethod != "Paypal" && paymentMethod != "الدفع عند الإستلام")
            {
                return BadRequest("طريقة الدفع غير صحيحة.");
            }

            
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound("لم يتم العثور على الطلب.");
            }

            
            order.PaymentMethod = paymentMethod;

            
            try
            {
                await _db.SaveChangesAsync();
                return Ok(new { message = "تم تحديث طريقة الدفع بنجاح." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "حدث خطأ أثناء تحديث طريقة الدفع.", error = ex.Message });
            }
        }


    }
}
