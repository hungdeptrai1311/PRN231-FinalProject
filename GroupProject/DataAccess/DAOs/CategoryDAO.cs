namespace DataAccess.DAOs;

using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryDAO
{
    public static List<Category> GetCategories()
    {
        List<Category> categoryList;
        try
        {
            using var context = new GroupProjectContext();
            categoryList = context.Categories.Include(c => c.Products).ToList();

            categoryList.ForEach(c =>
            {
                if (c.Products != null)
                {
                    foreach (var p in c.Products)
                    {
                        p.Category = null;
                    }
                }

                ;
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return categoryList;
    }

    public static Category? GetCategoryById(int id)
    {
        var category = new Category();
        try
        {
            using var context = new GroupProjectContext();
            category = context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);

            if (category?.Products != null)
            {
                foreach (var p in category.Products)
                {
                    p.Category = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return category;
    }

    public static void AddCategory(Category category)
    {
        try
        {
            using var context = new GroupProjectContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void UpdateCategory(Category category)
    {
        try
        {
            using var context = new GroupProjectContext();
            var       c       = GetCategoryById(category.CategoryId);

            if (c == null) return;
            c.CategoryName  = category.CategoryName;
            c.CategoryImage = category.CategoryImage;

            context.Categories.Update(c);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void DeleteCategory(int id)
    {
        try
        {
            var       c       = GetCategoryById(id);
            using var context = new GroupProjectContext();
            context.Categories.Remove(c!);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}