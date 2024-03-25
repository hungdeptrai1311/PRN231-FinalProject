using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using DataAccess.Repositories.UserRepo;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GroupProjectWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public UserController(IUserRepository userRepository,
                              IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

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

        [HttpGet]
        public IActionResult CheckLogin(string email, string password)
        {
            var user = this.userRepository.CheckLogin(email, password);
            return user != null ? Ok(this.GenerateJwtAToken(user)) : this.NotFound();
        }

        private string GenerateJwtAToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration.GetSection("Jwt:Key").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

