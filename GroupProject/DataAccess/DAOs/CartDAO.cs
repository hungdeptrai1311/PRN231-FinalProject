﻿namespace DataAccess.DAOs;

using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

public class CartDAO
{
    public static List<Cart> GetCartsByUserId(int userId)
    {
        List<Cart> cartList;
        try
        {
            using var context = new GroupProjectContext();
            cartList = context.Carts.Where(c => c.UserId == userId).Include(c => c.Product).ThenInclude(p => p.Brand).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return cartList;
    }

    public static Cart GetCartsByUserIdAndProductId(int userId, int productId)
    {
        var cart = new Cart();
        try
        {
            using var context = new GroupProjectContext();
            cart = context.Carts.Include(c => c.Product).ThenInclude(p => p.Brand).FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return cart;
    }

    public static Cart? GetCartById(int id)
    {
        var cart = new Cart();
        try
        {
            using var context = new GroupProjectContext();
            cart = context.Carts.Include(c => c.Product).FirstOrDefault(c => c.CartId == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return cart;
    }

    public static void AddCart(Cart cart)
    {
        try
        {
            using var context = new GroupProjectContext();
            context.Carts.Add(cart);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void UpdateCart(Cart cart)
    {
        try
        {
            using var context = new GroupProjectContext();
            var       c       = GetCartById(cart.CartId);

            if (c == null) return;
            c.Quantity  = cart.Quantity;

            context.Carts.Update(c);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void DeleteCart(int id)
    {
        try
        {
            var       c       = GetCartById(id);
            using var context = new GroupProjectContext();
            context.Carts.Remove(c!);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}