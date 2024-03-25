using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUsers() => UserDAO.GetAllUsers();
        public List<User> GetUserByName(string name) => UserDAO.GetUserByName(name);
        public User GetUserByID(int id) => UserDAO.GetUserByID(id);
        public void AddUser(User user) => UserDAO.AddUser(user);
        public void DeleteUser(int id)  => UserDAO.DeleteUser(id);
        public void UpdateUser(User user) => UserDAO.UpdateUser(user);

        public User CheckLogin(string email, string password) => UserDAO.CheckLogin(email, password);
    }
}
