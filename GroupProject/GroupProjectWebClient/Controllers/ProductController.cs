using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace GroupProjectWebClient.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await this.GetProductByIdAsync(id);
            var brands = await this.GetBrandsAsync();
            var brand = await this.GetBrandByIdAsync((int)product.BrandId);
            var user = await this.GetUserFromToken();

            ViewBag.Brands = brands;
            ViewBag.Brand = brand;
            ViewBag.User = user;

            return View(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/Products/GetProductById?id={id}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<Product>(data);
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

        public async Task<User> GetUserFromToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(HttpContext.Session.GetString("token"));
            int id = int.Parse(((JwtSecurityToken)jsonToken).Claims.FirstOrDefault(e => e.Type == "nameid")?.Value!);
            var user = await this.GetUserByUserIdAsync(id);
            return user;
        }
    }
}
