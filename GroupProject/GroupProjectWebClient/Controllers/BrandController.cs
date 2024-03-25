using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace GroupProjectWebClient.Controllers
{
    public class BrandController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            var brands = await this.GetBrandsAsync();
            var brand = await this.GetBrandByIdAsync(id);
            var products = (await this.GetProductsByBrandAsync(id)).Where(p => p.Quantity > 0).ToList();
            var user = await this.GetUserFromToken();

            ViewBag.Brands = brands;
            ViewBag.Brand = brand;
            ViewBag.User = user;

            return View(products);
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/Brands/GetBrandById?id={id}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<Brand>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return null!;
        }

        public async Task<List<Product>> GetProductsByBrandAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/Products/GetProductsByBrand?brandId={id}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
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
                string link = $"http://localhost:5152/api/Brands/GetBrands";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
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

        public async Task<User> GetUserFromToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(HttpContext.Session.GetString("token"));
            int id = int.Parse(((JwtSecurityToken)jsonToken).Claims.FirstOrDefault(e => e.Type == "nameid")?.Value!);
            var user = await this.GetUserByUserIdAsync(id);
            return user;
        }

        public async Task<User> GetUserByUserIdAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/User/GetUserById?id={id}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
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
    }
}
