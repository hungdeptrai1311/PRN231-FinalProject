using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetailList;
            try
            {
                using var context = new GroupProjectContext();
                orderDetailList = context.OrderDetails.Include(o => o.Product).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderDetailList;
        }

        public static OrderDetail? GetOrderDetailById(int id)
        {
            var orderDetail = new OrderDetail();
            try
            {
                using var context = new GroupProjectContext();
                orderDetail = context.OrderDetails.Include(o => o.Product).FirstOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderDetail;
        }

        public static void AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using var context = new GroupProjectContext();
                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using var context = new GroupProjectContext();
                var o = GetOrderDetailById(orderDetail.OrderDetailId);

                if (o == null) return;
                o.ProductId = orderDetail.ProductId;
                o.OrderId = orderDetail.OrderId;
                o.Quantity = orderDetail.Quantity;

                context.OrderDetails.Update(o);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderDetail(int id)
        {
            try
            {
                var o = GetOrderDetailById(id);
                using var context = new GroupProjectContext();
                context.OrderDetails.Remove(o!);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
