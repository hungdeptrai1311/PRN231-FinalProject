using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;

namespace GroupProjectWebClient.Controllers
{
    public class UserController : Controller
    {
        public async Task<User> GetUserFromToken()
        {
            var handler   = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(HttpContext.Session.GetString("token"));
            int id        = int.Parse(((JwtSecurityToken)jsonToken).Claims.FirstOrDefault(e => e.Type == "nameid")?.Value!);
            var user      = await this.GetUserByUserIdAsync(id);
            return user;
        }

        public IActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (await this.CheckLogin(email, password))
            {
                var user = await this.GetUserFromToken();

                if (user?.RoleId == 1) return RedirectToAction("Dashboard", "Admin");
                else return RedirectToAction("UserHomePage", "Home", new { id = user?.UserId });
                ;
            }

            return RedirectToAction(nameof(Login), new { message = "Your email or password is wrong" });
        }

        public IActionResult Register(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }


        public async Task<IActionResult> Profile(string message = "")
        {
            var user = await this.GetUserFromToken();
            var brands = await this.GetBrandsAsync();
            var orders = await this.GetOrdersByUserIdAsync(user.UserId);

            ViewBag.Message = message;
            ViewBag.Brands = brands;
            ViewBag.Orders = orders;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User user)
        {
            if (await this.EditUserAsync(user)) return RedirectToAction(nameof(Profile), routeValues: new { id = user.UserId, message = "Edit successfully!!!" });
            else return RedirectToAction(nameof(Profile), routeValues: new { id                                = user.UserId, message = "Edit fail" });
        }

        public async Task<IActionResult> AdminUserManagement()
        {
            var members = (await this.GetUsersAsync()).Where(m => m.RoleId != 1).ToList();
            return View(members);
        }

        public async Task<IActionResult> UpdateUser(int userId, string message = "")
        {
            var member = await this.GetUserByUserIdAsync(userId);
            ViewBag.Noti = message;
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (await this.EditUserAsync(user)) return RedirectToAction(nameof(UpdateUser), new { userId = user.UserId, noti = "Update successfully!!!" });
            else return RedirectToAction(nameof(UpdateUser), new { userId                                = user.UserId, noti = "Update fail" });
        }

        public async Task<IActionResult> DeleteUser(int userId)
        {

            await this.RemoveUserAsync(userId);
            return RedirectToAction(nameof(AdminUserManagement));
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var members = await this.GetUsersAsync();
            await this.InsertUserAsync(user);
            return RedirectToAction(nameof(AdminUserManagement));
        }

        public async Task<bool> CheckLogin(string email, string password)
        {
            try
            {
                string link = $"http://localhost:5152/api/User/CheckLogin?email={email}&password={password}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        if (res.StatusCode is System.Net.HttpStatusCode.OK)
                        {
                            HttpContext.Session.SetString("token", res.Content.ReadAsStringAsync().Result.ToString());
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                string link = $"http://localhost:5152/api/User/GetAllUsers";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<List<User>>(data);
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

        public async Task<bool> EditUserAsync(User member)
        {
            try
            {
                string link = $"http://localhost:5152/api/User/UpdateUser";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PutAsJsonAsync(link, member))
                    {
                        return res.IsSuccessStatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public async Task RemoveUserAsync(int id)
        {
            try
            {
                string link = $"http://localhost:5152/api/User/DeleteUser?id={id}";
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

        public async Task InsertUserAsync(User user)
        {
            try
            {
                string link = $"http://localhost:5152/api/User/AddUser";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(link, user))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

		public async Task<List<Category>> GetCategoriesAsync()
		{
			try
			{
				string link = $"http://localhost:5152/api/Categories/GetCategories";
				using (HttpClient client = new HttpClient())
				{
					using (HttpResponseMessage res = await client.GetAsync(link))
					{
						using (HttpContent content = res.Content)
						{
							string data = content.ReadAsStringAsync().Result;
							return JsonConvert.DeserializeObject<List<Category>>(data);
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

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            try
            {
                string link = $"http://localhost:5152/api/Orders/GetOrdersByUserId?id={userId}";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<List<Order>>(data);
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
