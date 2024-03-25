namespace GroupProjectWebClient.Controllers;

using System.IdentityModel.Tokens.Jwt;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        return this.View();
    }

    public async Task<IActionResult> ManageProduct(int page = 0, string error = "")
    {
        var products = await this.GetAllProductsAsync();
        var brands   = await this.GetAllBrandsAsync();

        return this.View("ManageProduct", new ManageProductViewModel
        {
            Products = (products ?? new List<Product>()).Skip(page * 3).Take(3),
            EndPage  = (products?.Count() ?? 0) / 3,
            Brands   = brands ?? new List<Brand>(),
            Error    = error,
        });
    }

    public async Task<IActionResult> EditProduct(int productId)
    {
        return this.View("EditProduct", new EditProductViewModel
        {
            Product = await this.GetProductByIdAsync(productId),
            Brands  = await this.GetAllBrandsAsync(),
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(Product product)
    {
        try
        {
            const string link   = "http://localhost:5152/api/Products/UpdateProduct";
            using var    client = new HttpClient();
            using var    res    = await client.PutAsJsonAsync(link, product);
        }
        catch (Exception e)
        {
            return await this.ManageProduct(error: e.Message);
        }

        return await this.ManageProduct();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        try
        {
            const string link   = "http://localhost:5152/api/Products/AddProduct";
            using var    client = new HttpClient();
            using var    res    = await client.PostAsJsonAsync(link, product);
        }
        catch (Exception e)
        {
            return await this.ManageProduct(error: e.Message);
        }

        return await this.ManageProduct();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var       link   = $"http://localhost:5152/api/Products/DeleteProduct?id={id}";
            using var client = new HttpClient();
            using var res    = await client.DeleteAsync(link);
        }
        catch (Exception e)
        {
            return await this.ManageProduct(error: e.Message);
        }

        return await this.ManageProduct();
    }

    public async Task<IActionResult> Customers()
    {
        var users = await this.GetAllUsersAsync();
        return this.View("Customers", new CustomerViewModel
        {
            Users = users ?? new List<User>(),
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var       link   = $"http://localhost:5152/api/User/DeleteUser?id={id}";
            using var client = new HttpClient();
            using var res    = await client.DeleteAsync(link);
        }
        catch (Exception e)
        {
            return await this.ManageProduct(error: e.Message);
        }

        return await this.ManageProduct();
    }

    // ReSharper disable once InconsistentNaming
    public IActionResult EditEULA()
    {
        return this.View();
    }

    public async Task<User?> GetUserFromToken()
    {
        var handler   = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(this.HttpContext.Session.GetString("token"));
        var id        = int.Parse(((JwtSecurityToken)jsonToken).Claims.FirstOrDefault(e => e.Type == "nameid")?.Value!);
        var user      = await this.GetUserByUserIdAsync(id);
        return user;
    }

    public async Task<User?> GetUserByUserIdAsync(int id)
    {
        try
        {
            var link = $"http://localhost:5152/api/User/GetUserById?id={id}";
            using (var client = new HttpClient())
            {
                using (var res = await client.GetAsync(link))
                {
                    using (var content = res.Content)
                    {
                        var data = content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<User>(data);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return null!;
    }

    public async Task<IEnumerable<Product>?> GetAllProductsAsync()
    {
        try
        {
            const string link    = "http://localhost:5152/api/Products/GetProducts";
            using var    client  = new HttpClient();
            using var    res     = await client.GetAsync(link);
            using var    content = res.Content;
            var          data    = content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(data);
        }
        catch (Exception ex)
        {
            // ignored
        }

        return null!;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            var       link    = $"http://localhost:5152/api/Products/GetProductById?id={id}";
            using var client  = new HttpClient();
            using var res     = await client.GetAsync(link);
            using var content = res.Content;
            var       data    = content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Product>(data);
        }
        catch (Exception ex)
        {
            // ignored
        }

        return null!;
    }

    public async Task<IEnumerable<Brand>?> GetAllBrandsAsync()
    {
        try
        {
            const string link    = "http://localhost:5152/api/Brands/GetBrands";
            using var    client  = new HttpClient();
            using var    res     = await client.GetAsync(link);
            using var    content = res.Content;
            var          data    = content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<Brand>>(data);
        }
        catch (Exception ex)
        {
            // ignored
        }

        return null!;
    }

    public async Task<IEnumerable<User>?> GetAllUsersAsync()
    {
        try
        {
            const string link    = "http://localhost:5152/api/User/GetAllUsers";
            using var    client  = new HttpClient();
            using var    res     = await client.GetAsync(link);
            using var    content = res.Content;
            var          data    = content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<User>>(data);
        }
        catch (Exception ex)
        {
            // ignored
        }

        return null!;
    }

    public class ManageProductViewModel
    {
        public string               Error    { get; set; }
        public IEnumerable<Brand>   Brands   { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int                  EndPage  { get; set; }
    }

    public class EditProductViewModel
    {
        public IEnumerable<Brand>? Brands  { get; set; }
        public Product?            Product { get; set; }
    }

    public class CustomerViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}