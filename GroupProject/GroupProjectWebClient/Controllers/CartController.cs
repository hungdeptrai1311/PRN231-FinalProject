using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace GroupProjectWebClient.Controllers
{
    public class CartController : Controller
    {
        public async Task<IActionResult> CartDetail()
        {
            var user = await this.GetUserFromToken();
            var carts = await this.GetCartByUserIdAsync(user.UserId);
            var brands = await this.GetBrandsAsync();

            ViewBag.Brands = brands;
            ViewBag.User = user;

            return View(carts);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var user = await this.GetUserFromToken();
            var oldCart = await this.GetCartByUserIdAndProductIdAsync(user.UserId, productId);
            if (oldCart != null)
            {
                oldCart.Quantity += quantity;

                await this.EditCartAsync(oldCart);
            }
            else
            {
                Cart cart = new Cart
                {
                    UserId = user.UserId,
                    ProductId = productId,
                    Quantity = quantity
                };

                await this.InsertCartAsync(cart);
            }

            return RedirectToAction(nameof(CartDetail));
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await this.GetUserFromToken();
            var carts = await this.GetCartByUserIdAsync(user.UserId);
            var brands = await this.GetBrandsAsync();

            ViewBag.Brands = brands;
            ViewBag.User = user;

            Order order = new Order
            {
                UserId = user.UserId,
                Date = DateTime.Now
            };

            await this.InsertOrderAsync(order);
            var orderId = await this.GetCurrentOrderId();
            carts.ForEach(async cart =>
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity
                };

                await this.InsertOrderDetailAsync(orderDetail);
            });

            carts.ForEach(async cart =>
            {
                await this.RemoveCartAsync(cart.CartId);
            });

            return View();
        }

        public async Task<User> GetUserFromToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(HttpContext.Session.GetString("token"));
            int id = int.Parse(((JwtSecurityToken)jsonToken).Claims.FirstOrDefault(e => e.Type == "nameid")?.Value!);
            var user = await this.GetUserByUserIdAsync(id);
            return user;
        }

        public async Task<List<Cart>> GetCartByUserIdAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/Carts/GetCartsByUserId?userId={id}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<List<Cart>>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return null!;
        }

        public async Task<Cart> GetCartByUserIdAndProductIdAsync(int userId, int productId)
        {
            try
            {
                string link = $"http://localhost:5152/api/Carts/GetCartsByUserIdAndProductId?userId={userId}&productId={productId}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<Cart>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return null!;
        }

        public async Task RemoveCartAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/Carts/DeleteCart?id={id}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.DeleteAsync(link))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
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

        public async Task<int> GetCurrentOrderId()
        {
            try
            {
                string link = $"http://localhost:5152/api/Orders/GetCurrentOrderId";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<int>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return 0;
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

        public async Task InsertCartAsync(Cart cart)
        {
            try
            {
                string link = $"http://localhost:5152/api/Carts/AddCart";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(link, cart))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task EditCartAsync(Cart cart)
        {
            try
            {
                string link = $"http://localhost:5152/api/Carts/UpdateCart";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PutAsJsonAsync(link, cart))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task InsertOrderAsync(Order order)
        {
            try
            {
                string link = $"http://localhost:5152/api/Orders/AddOrder";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(link, order))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task InsertOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                string link = $"http://localhost:5152/api/OrderDetails/AddOrderDetail";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(link, orderDetail))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
