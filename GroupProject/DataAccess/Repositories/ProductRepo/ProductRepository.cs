namespace DataAccess.Repositories.ProductRepo;

using BusinessObject.Models;
using DataAccess.DAOs;

public class ProductRepository : IProductRepository
{
    public List<Product> GetProducts()                         => ProductDAO.GetProducts();
    public List<Product> GetProductsByBrand(int brandId)       => ProductDAO.GetProductsByBrand(brandId);
    public List<Product> GetProductsByCategory(int categoryId) => ProductDAO.GetProductsByCategory(categoryId);
    public Product?      GetProductById(int id)                => ProductDAO.GetProductById(id);
    public void          AddProduct(Product product)           => ProductDAO.AddProduct(product);
    public void          UpdateProduct(Product product)        => ProductDAO.UpdateProduct(product);
    public void          DeleteProduct(int id)                 => ProductDAO.DeleteProduct(id);
}