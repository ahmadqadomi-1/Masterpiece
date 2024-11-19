using masterpieceDashboard.Server.Models;
using masterpieceDashboard.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace masterpieceDashboard.Server.Controllers
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
                .Include(o => o.OrderStatusNavigation)
                .Select(o => new
                {
                    o.OrderId,
                    UserName = o.User != null ? o.User.UserName : null,
                    Phone = o.User != null ? o.User.PhoneNumber : null,
                    OrderStatus = o.OrderStatusNavigation != null ? o.OrderStatusNavigation.StatusName : "Pending",
                    o.PaymentMethod,
                    o.OrderDate,
                    o.Comment,
                    o.TotalAmount
                })
                .ToList();

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderStatusNavigation)
                .Where(o => o.OrderId == orderId)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.PaymentMethod,
                    OrderStatus = o.OrderStatusNavigation != null ? o.OrderStatusNavigation.StatusName : "Pending",
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


        [HttpGet("Status/{orderId}")]
        public IActionResult GetStatusById(int orderId)
        {
            var order = _db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderStatusNavigation)
                .Where(o => o.OrderId == orderId)
                .Select(o => new
                {
                    OrderStatus = o.OrderStatusNavigation != null ? o.OrderStatusNavigation.StatusName : "Pending",
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
                .Include(o => o.OrderStatusNavigation)
                .Where(o => o.UserId == userId)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.PaymentMethod,
                    OrderStatus = o.OrderStatusNavigation != null ? o.OrderStatusNavigation.StatusName : "Pending",
                    o.OrderDate,
                    o.OrderStatusId,
                    o.Comment,
                    UserName = o.User != null ? o.User.UserName : null,
                    UserEmail = o.User != null ? o.User.Email : null,
                    Phone = o.User != null ? o.User.PhoneNumber : null
                })
                .ToList();

            if (!orders.Any())
            {
                return NotFound("No orders found for this user.");
            }

            return Ok(orders);
        }

        [HttpPut("UpdateOrderStatus/{orderId}")]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] string statusName)
        {
            // التحقق من صحة حالة الطلب
            if (string.IsNullOrWhiteSpace(statusName))
            {
                return BadRequest("Invalid order status.");
            }

            var validStatuses = new[] { "Pending", "Approved", "InPacking", "Shipping", "Delivered" };

            if (!validStatuses.Contains(statusName))
            {
                return BadRequest("Invalid order status.");
            }

            // البحث عن الطلب في قاعدة البيانات
            var order = _db.Orders
                           .Include(o => o.OrderStatusNavigation)
                           .Include(o => o.User) // تضمين العلاقة مع User
                           .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // التحقق من وجود مستخدم مرتبط
            if (order.User == null)
            {
                return BadRequest("No user is associated with this order.");
            }

            // التحقق من وجود البريد الإلكتروني
            if (string.IsNullOrWhiteSpace(order.User.Email))
            {
                return BadRequest("The associated user does not have an email address.");
            }

            // البحث عن حالة الطلب الجديدة
            var newStatus = _db.OrderStatuses
                               .FirstOrDefault(os => os.StatusName == statusName);

            if (newStatus == null)
            {
                return NotFound("Specified order status not found.");
            }

            // تحديث حالة الطلب
            order.OrderStatusId = newStatus.OrderStatusId;
            _db.SaveChanges();

            // إرسال البريد الإلكتروني للمستخدم بعد تحديث الحالة
            var userEmail = order.User.Email;
            var subject = "Order Status Updated";
            var body = $"Dear {order.User.UserName},\n\nYour order status has been updated to: {statusName}.\n\nThank you for shopping with us!";

            try
            {
                // استدعاء خدمة البريد الإلكتروني لإرسال البريد للمستخدم
                EmailService.SendEmail(userEmail, subject, body);
            }
            catch (Exception ex)
            {
                // إذا حدث خطأ أثناء إرسال البريد الإلكتروني، يمكنك تسجيل الخطأ هنا
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

            // إرجاع استجابة بنجاح التحديث
            return Ok(new { message = "Order status updated successfully." });
        }





        [HttpGet("GetOrderDetails/{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            var order = _db.Orders
                            .Include(o => o.CartItems)
                            .ThenInclude(ci => ci.Product)
                            .Where(o => o.OrderId == orderId)
                            .Select(o => new
                            {
                                o.OrderId,
                                o.TotalAmount,
                                o.PaymentMethod,
                                o.OrderStatus,
                                o.OrderDate,
                                o.Comment,
                                Products = o.CartItems.Select(ci => new
                                {
                                    ci.Product.ProductName,
                                    ci.Product.ProductImage,
                                    ci.Product.Price,
                                    ci.Quantity,
                                    TotalAmount = ci.Product.Price * ci.Quantity
                                }).ToList()
                            })
                            .FirstOrDefault();

            if (order == null)
            {
                return NotFound("لم يتم العثور على الطلب.");
            }

            return Ok(order);
        }

        [HttpDelete("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var order = _db.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
