namespace DataAccess.Repositories.BrandRepo;

using BusinessObject.Models;

public interface IBrandRepository
{
    List<Brand> GetBrands();
    Brand?      GetBrandById(int id);
    void        AddBrand(Brand brand);
    void        UpdateBrand(Brand brand);
    void        DeleteBrand(int id);
}