using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repositories.UserRepo
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public List<User> GetUserByName(string name);
        public  User GetUserByID(int id);
        public void AddUser(User user);
        public void DeleteUser(int id);
        public void UpdateUser(User user);
        User CheckLogin(string email, string password);

    }
}
