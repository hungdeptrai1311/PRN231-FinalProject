namespace GroupProjectApi.Controllers;

using BusinessObject.Models;
using DataAccess.Repositories.CategoryRepo;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    #region Inject
    
    private readonly ICategoryRepository categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository) { this.categoryRepository = categoryRepository; }

    #endregion

    [HttpGet]
    public IEnumerable<Category> GetCategories() => this.categoryRepository.GetCategories();

    [HttpGet]
    public Category? GetCategoryById(int id) => this.categoryRepository.GetCategoryById(id);

    [HttpPost]
    public IActionResult AddCategory(Category category)
    {
        this.categoryRepository.AddCategory(category);
        return this.Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateCategory(Category category)
    {
        this.categoryRepository.UpdateCategory(category);
        return this.Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteCategory(int id)
    {
        this.categoryRepository.DeleteCategory(id);
        return this.Ok();
    }
}