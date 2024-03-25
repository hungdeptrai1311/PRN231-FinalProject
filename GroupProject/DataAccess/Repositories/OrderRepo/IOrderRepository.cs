using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderRepo
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order? GetOrderById(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        int GetCurrentOrderId();
        List<Order> GetOrdersByUserId(int id);
    }
}
