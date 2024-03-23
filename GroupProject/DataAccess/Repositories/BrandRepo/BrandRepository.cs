namespace DataAccess.Repositories.BrandRepo;

using BusinessObject.Models;
using DataAccess.DAOs;

public class BrandRepository : IBrandRepository
{
    public List<Brand> GetBrands()              => BrandDAO.GetBrands();
    public Brand?      GetBrandById(int id)     => BrandDAO.GetBrandById(id);
    public void        AddBrand(Brand brand)    => BrandDAO.AddBrand(brand);
    public void        UpdateBrand(Brand brand) => BrandDAO.UpdateBrand(brand);
    public void        DeleteBrand(int id)      => BrandDAO.DeleteBrand(id);
}