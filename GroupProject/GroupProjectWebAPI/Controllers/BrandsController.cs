namespace GroupProjectApi.Controllers;

using BusinessObject.Models;
using DataAccess.Repositories.BrandRepo;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class BrandsController : ControllerBase
{
    #region Inject
    
    private readonly IBrandRepository brandRepository;

    public BrandsController(IBrandRepository brandRepository) { this.brandRepository = brandRepository; }

    #endregion

    [HttpGet]
    public IEnumerable<Brand> GetBrands() => this.brandRepository.GetBrands();

    [HttpGet]
    public Brand? GetBrandById(int id) => this.brandRepository.GetBrandById(id);

    [HttpPost]
    public IActionResult AddBrand(Brand brand)
    {
        this.brandRepository.AddBrand(brand);
        return this.Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateBrand(Brand brand)
    {
        this.brandRepository.UpdateBrand(brand);
        return this.Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteBrand(int id)
    {
        this.brandRepository.DeleteBrand(id);
        return this.Ok();
    }
}