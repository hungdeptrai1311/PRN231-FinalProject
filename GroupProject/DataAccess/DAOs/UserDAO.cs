using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class UserDAO
    {
        //Get all list users
        public static List<User> GetAllUsers()
        {
            List<User> userList;
            try
            {
                using var context = new GroupProjectContext();
                userList = context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userList;
        }

        //Get user by name
        public static List<User> GetUserByName(string name)
        {
            List<User> AllUserList = null;
            List<User> UserByNameList = new List<User> ();
            try
            {
                using var context = new GroupProjectContext();
                AllUserList = context.Users.ToList();

                foreach (User user in AllUserList)
                {
                    if (user.Name.Equals(name))
                    {
                        UserByNameList.Add(user);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return UserByNameList;
        }

        // Get user by id
        public static User GetUserByID(int id)
        {
            User UserByNameList = new User();
            try
            {
                using var context = new GroupProjectContext();
                UserByNameList = context.Users.FirstOrDefault(x => x.UserId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return UserByNameList;
        }

        // Add user
        public static void AddUser(User user)
        {
            try
            {
                using var context = new GroupProjectContext();
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete user
        public static void DeleteUser(int id)
        {
            try
            {
                var user = GetUserByID(id);
                using var context = new GroupProjectContext();
                context.Users.Remove(user!);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //Update User
        public static void UpdateUser(User user)
        {
            try
            {
                using var context = new GroupProjectContext();
                var u = GetUserByID(user.UserId);

                if (u == null) return;
                u.Name = user.Name;
                u.Email = user.Email;
                u.Password = user.Password;
                u.Address = user.Address;
                u.Phone = user.Phone;
                u.RoleId = user.RoleId;
                

                context.Users.Update(u);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
