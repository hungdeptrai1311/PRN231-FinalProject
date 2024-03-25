using BusinessObject.Models;
using DataAccess.Repositories.OrderDetailRepo;
using DataAccess.Repositories.OrderRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupProjectWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        #region Inject
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderDetailsController(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }
        #endregion

        [HttpGet]
        public IEnumerable<OrderDetail> GetOrderDetails() => this.orderDetailRepository.GetOrderDetails();

        [HttpGet]
        public OrderDetail? GetOrderDetailById(int id) => this.orderDetailRepository.GetOrderDetailById(id);

        [HttpPost]
        public IActionResult AddOrderDetail(OrderDetail orderDetail)
        {
            this.orderDetailRepository.AddOrderDetail(orderDetail);
            return this.Ok();
        }

        [HttpPut]
        public IActionResult UpdateOrderDetail(OrderDetail orderDetail)
        {
            this.orderDetailRepository.UpdateOrderDetail(orderDetail);
            return this.Ok();
        }

        [HttpDelete]
        public IActionResult DeleteOrderDetail(int id)
        {
            this.orderDetailRepository.DeleteOrderDetail(id);
            return this.Ok();
        }
    }
}
