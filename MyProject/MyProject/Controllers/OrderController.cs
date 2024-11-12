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
        public IActionResult Order()
        {
            var OO = _db.Orders.ToList();
            return Ok(OO);
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
