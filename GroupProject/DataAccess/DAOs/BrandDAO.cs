namespace DataAccess.DAOs;

using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

public class BrandDAO
{
    public static List<Brand> GetBrands()
    {
        List<Brand> brandList;
        try
        {
            using var context = new GroupProjectContext();
            brandList = context.Brands.Include(b => b.Products).ToList();

            brandList.ForEach(b =>
            {
                if (b.Products != null)
                {
                    foreach (var p in b.Products)
                    {
                        p.Brand = null;
                    }
                }

                ;
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return brandList;
    }

    public static Brand? GetBrandById(int id)
    {
        var brand = new Brand();
        try
        {
            using var context = new GroupProjectContext();
            brand = context.Brands.Include(b => b.Products).FirstOrDefault(b => b.BrandId == id);

            if (brand?.Products != null)
            {
                foreach (var p in brand.Products)
                {
                    p.Brand = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return brand;
    }

    public static void AddBrand(Brand brand)
    {
        try
        {
            using var context = new GroupProjectContext();
            context.Brands.Add(brand);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void UpdateBrand(Brand brand)
    {
        try
        {
            using var context = new GroupProjectContext();
            var       b       = GetBrandById(brand.BrandId);

            if (b == null) return;
            b.BrandName   = brand.BrandName;
            b.Description = brand.Description;
            b.BrandImage  = brand.BrandImage;

            context.Brands.Update(b);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void DeleteBrand(int id)
    {
        try
        {
            var       b       = GetBrandById(id);
            using var context = new GroupProjectContext();
            context.Brands.Remove(b!);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}