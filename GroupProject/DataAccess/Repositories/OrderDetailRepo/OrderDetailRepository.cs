using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderDetailRepo
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void AddOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.AddOrderDetail(orderDetail);

        public void DeleteOrderDetail(int id) => OrderDetailDAO.DeleteOrderDetail(id);

        public OrderDetail? GetOrderDetailById(int id) => OrderDetailDAO.GetOrderDetailById(id);

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();

        public void UpdateOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.UpdateOrderDetail(orderDetail);
    }
}
