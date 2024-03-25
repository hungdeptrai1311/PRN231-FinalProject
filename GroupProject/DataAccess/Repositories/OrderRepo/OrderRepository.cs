using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order) => OrderDAO.AddOrder(order);

        public void DeleteOrder(int id) => OrderDAO.DeleteOrder(id);

        public int GetCurrentOrderId() => OrderDAO.GetCurrentOrderId();

        public Order? GetOrderById(int id) => OrderDAO.GetOrderById(id);

        public List<Order> GetOrders() => OrderDAO.GetOrders();

        public List<Order> GetOrdersByUserId(int id) => OrderDAO.GetOrdersByUserId(id);

        public void UpdateOrder(Order order) => OrderDAO.UpdateOrder(order);
    }
}
