namespace DataAccess.Repositories.ProductRepo;

using BusinessObject.Models;

public interface IProductRepository
{
    List<Product> GetProducts();
    List<Product> GetProductsByBrand(int brandId);
    List<Product> GetProductsByCategory(int categoryId);
    Product?      GetProductById(int id);
    void          AddProduct(Product product);
    void          UpdateProduct(Product product);
    void          DeleteProduct(int id);
}