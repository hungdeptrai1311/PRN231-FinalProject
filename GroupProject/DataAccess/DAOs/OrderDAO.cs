using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class OrderDAO
    {
        public static List<Order> GetOrders()
        {
            List<Order> orderList;
            try
            {
                using var context = new GroupProjectContext();
                orderList = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderList;
        }

        public static Order? GetOrderById(int id)
        {
            var order = new Order();
            try
            {
                using var context = new GroupProjectContext();
                order = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return order;
        }

        public static List<Order> GetOrdersByUserId(int id)
        {
            List<Order> orderList;
            try
            {
                using var context = new GroupProjectContext();
                orderList = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).Where(o => o.UserId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderList;
        }

        public static void AddOrder(Order order)
        {
            try
            {
                using var context = new GroupProjectContext();
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrder(Order order)
        {
            try
            {
                using var context = new GroupProjectContext();
                var o = GetOrderById(order.OrderId);

                if (o == null) return;
                o.Date = order.Date;

                context.Orders.Update(o);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrder(int id)
        {
            try
            {
                var o = GetOrderById(id);
                using var context = new GroupProjectContext();
                context.Orders.Remove(o!);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int GetCurrentOrderId()
        {
            var order = new Order();
            try
            {
                using var context = new GroupProjectContext();
                order = context.Orders.FirstOrDefault(o => o.OrderId == context.Orders.Max(o => o.OrderId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return order!.OrderId;
        }
    }
}
