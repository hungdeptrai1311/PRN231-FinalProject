namespace DataAccess.DAOs;

using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

public class ProductDAO
{
    public static List<Product> GetProducts()
    {
        List<Product> productList;
        try
        {
            using var context = new GroupProjectContext();
            productList = context.Products.Include(p => p.Brand).Include(p => p.Category).ToList();

            productList.ForEach(p =>
            {
                if (p.Brand != null) p.Brand.Products       = null;
                if (p.Category != null) p.Category.Products = null;
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return productList;
    }

    public static List<Product> GetProductsByCategory(int categoryId)
    {
        List<Product> productList;
        try
        {
            using var context = new GroupProjectContext();
            productList = context.Products.Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            productList.ForEach(p =>
            {
                if (p.Brand != null) p.Brand.Products       = null;
                if (p.Category != null) p.Category.Products = null;
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return productList;
    }

    public static List<Product> GetProductsByBrand(int brandId)
    {
        List<Product> productList;
        try
        {
            using var context = new GroupProjectContext();
            productList = context.Products.Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.BrandId == brandId)
                .ToList();

            productList.ForEach(p =>
            {
                if (p.Brand != null) p.Brand.Products       = null;
                if (p.Category != null) p.Category.Products = null;
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return productList;
    }

    public static Product? GetProductById(int id)
    {
        var product = new Product();
        try
        {
            using var context = new GroupProjectContext();
            product = context.Products.Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                if (product.Brand != null) product.Brand.Products       = null;
                if (product.Category != null) product.Category.Products = null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return product;
    }

    public static void AddProduct(Product product)
    {
        try
        {
            using var context = new GroupProjectContext();
            context.Products.Add(product);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void UpdateProduct(Product product)
    {
        try
        {
            using var context = new GroupProjectContext();
            var       p       = GetProductById(product.ProductId);

            if (p == null) return;
            p.ProductName  = product.ProductName;
            p.Description  = product.Description;
            p.Price        = product.Price;
            p.Quantity     = product.Quantity;
            p.ProductImage = product.ProductImage;

            context.Products.Update(p);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void DeleteProduct(int id)
    {
        try
        {
            var       p       = GetProductById(id);
            using var context = new GroupProjectContext();
            context.Products.Remove(p!);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}