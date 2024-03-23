namespace GroupProjectApi.Controllers;

using BusinessObject.Models;
using DataAccess.Repositories.ProductRepo;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    #region Inject
    
    private readonly IProductRepository productRepository;

    public ProductsController(IProductRepository productRepository) { this.productRepository = productRepository; }

    #endregion

    [HttpGet]
    public IEnumerable<Product> GetProducts() => this.productRepository.GetProducts();
    
    [HttpGet]
    public IEnumerable<Product> GetProductsByBrand(int brandId) => this.productRepository.GetProductsByBrand(brandId);
    
    [HttpGet]
    public IEnumerable<Product> GetProductsByCategory(int categoryId) => this.productRepository.GetProductsByCategory(categoryId);

    [HttpGet]
    public Product? GetProductById(int id) => this.productRepository.GetProductById(id);

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        this.productRepository.AddProduct(product);
        return this.Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateProduct(Product product)
    {
        this.productRepository.UpdateProduct(product);
        return this.Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteProduct(int id)
    {
        this.productRepository.DeleteProduct(id);
        return this.Ok();
    }
}