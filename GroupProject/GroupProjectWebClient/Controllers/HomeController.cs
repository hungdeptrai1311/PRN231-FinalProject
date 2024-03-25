namespace GroupProjectWebClient.Controllers;

using System.IdentityModel.Tokens.Jwt;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return this.View("UserHomePage");
    }

    public async Task<IActionResult> UserHomePage()
    {
        var products = await this.GetProductsAsync();
        var brands   = await this.GetBrandsAsync();
        var user     = await this.GetUserFromToken();

        this.ViewBag.User   = user;
        this.ViewBag.Brands = brands;

        return this.View(products);
    }

    public async Task<User> GetUserFromToken()
    {
        var handler   = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(this.HttpContext.Session.GetString("token"));
        var id        = int.Parse(((JwtSecurityToken)jsonToken).Claims.FirstOrDefault(e => e.Type == "nameid")?.Value!);
        var user      = await this.GetUserByUserIdAsync(id);
        return user;
    }

    public async Task<User> GetUserByUserIdAsync(int id)
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
        }

        return null!;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            var link = "http://localhost:5152/api/Products/GetProducts";
            using (var client = new HttpClient())
            {
                using (var res = await client.GetAsync(link))
                {
                    using (var content = res.Content)
                    {
                        var data = content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Product>>(data);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }

        return null!;
    }

    public async Task<List<Brand>> GetBrandsAsync()
    {
        try
        {
            var link = "http://localhost:5152/api/Brands/GetBrands";
            using (var client = new HttpClient())
            {
                using (var res = await client.GetAsync(link))
                {
                    using (var content = res.Content)
                    {
                        var data = content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Brand>>(data);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }

        return null!;
    }
}