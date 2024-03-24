using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using DataAccess.Repositories.UserRepo;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories.CategoryRepo;
using DataAccess.DAOs;

namespace GroupProjectWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository) { this.userRepository = userRepository; }

        [HttpGet]
        public IEnumerable<User> GetAllUsers() => this.userRepository.GetAllUsers();

        [HttpGet]
        public User? GetUserById(int id) => this.userRepository.GetUserByID(id);

        [HttpGet]
        public IEnumerable<User> GetUserByName(string name) => this.userRepository.GetUserByName(name);

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            this.userRepository.AddUser(user);
            return this.Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            this.userRepository.UpdateUser(user);
            return this.Ok();
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            this.userRepository.DeleteUser(id);
            return this.Ok();
        }
    }




}

