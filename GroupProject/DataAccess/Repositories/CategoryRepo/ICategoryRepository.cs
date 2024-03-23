namespace DataAccess.Repositories.CategoryRepo;

using BusinessObject.Models;

public interface ICategoryRepository
{
    List<Category> GetCategories();
    Category?      GetCategoryById(int id);
    void           AddCategory(Category category);
    void           UpdateCategory(Category category);
    void           DeleteCategory(int id);
}