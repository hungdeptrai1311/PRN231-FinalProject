using BusinessObject.Models;
using DataAccess.Repositories.OrderRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupProjectWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Inject
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        #endregion

        [HttpGet]
        public IEnumerable<Order> GetOrders() => this.orderRepository.GetOrders();

        [HttpGet]
        public Order? GetOrderById(int id) => this.orderRepository.GetOrderById(id);

        [HttpGet]
        public IEnumerable<Order> GetOrdersByUserId(int id) => this.orderRepository.GetOrdersByUserId(id);

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            this.orderRepository.AddOrder(order);
            return this.Ok();
        }

        [HttpPut]
        public IActionResult UpdateOrder(Order order)
        {
            this.orderRepository.UpdateOrder(order);
            return this.Ok();
        }

        [HttpDelete]
        public IActionResult DeleteOrder(int id)
        {
            this.orderRepository.DeleteOrder(id);
            return this.Ok();
        }

        [HttpGet]
        public int GetCurrentOrderId() => this.orderRepository.GetCurrentOrderId();
    }
}
