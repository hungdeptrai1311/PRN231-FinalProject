using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace GroupProjectWebClient.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var m = await this.GetUserByEmailAndPasswordAsync(email, password);
            if (m == null || m?.UserId == 0) return RedirectToAction(nameof(Login), new { message = "Your email or password is wrong" });
            else
            {
                if (m?.RoleId == 1) return RedirectToAction(nameof(AdminPage), new { id = m?.UserId });
                else return RedirectToAction(nameof(UserPage), new { id = m?.UserId }); ;
            }
        }

        public async Task<IActionResult> UserPage(int id)
        {
            var user = await this.GetUserByUserIdAsync(id);
            return View(user);
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        public async Task<IActionResult> Profile(int id, string message = "")
        {
            var member = await this.GetUserByUserIdAsync(id);
            ViewBag.Noti = message;
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User user)
        {
            if (await this.EditUserAsync(user)) return RedirectToAction(nameof(Profile), routeValues: new { id = user.UserId, message = "Edit successfully!!!" });
            else return RedirectToAction(nameof(Profile), routeValues: new { id = user.UserId, message = "Edit fail" });
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
            else return RedirectToAction(nameof(UpdateUser), new { userId = user.UserId, noti = "Update fail" });
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

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                string link = $"http://localhost:5052/api/Member/FindMemberByEmailAndPassword?email={email}&password={password}";
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

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                string link = $"http://localhost:5052/api/Member";
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
                string link = $"http://localhost:5052/api/Member/FindMemberById?id={id}";
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
                string link = $"http://localhost:5052/api/Member";
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
                string link = $"http://localhost:5052/api/Member?id={id}";
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
                string link = $"http://localhost:5052/api/Member";
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
    }
}
