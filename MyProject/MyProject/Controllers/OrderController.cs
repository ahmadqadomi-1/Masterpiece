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
    }
}
