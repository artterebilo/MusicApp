using DataBase.Migrations;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories;

public static class UserRepository
{
    public static bool IsUniqueLogin(string login)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var check = db.Users.Where(x => x.Login == login);

            if (check.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
    public static bool IsUniqueEmail(string email)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var check = db.Users.Where(x => x.Email == email);

            if (check.Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
    public static List<UserModel> GetAllUsers()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Users.ToList();
        }
    }

    public static void CreateUser(UserModel user)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var newUser = new UserModel
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
            };
            db.Users.Add(newUser);
            db.SaveChanges();
        }
    }

    public static UserModel GetUserById(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }
    }

    public static void UpdateUser(UserModel user)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdate(u => u
                .SetProperty(u => u.Password, user.Password)
                .SetProperty(u => u.FirstName, user.FirstName)
                .SetProperty(u => u.LastName, user.LastName)
                .SetProperty(u => u.DateOfBirth, user.DateOfBirth));
            db.SaveChanges();
        }
    }

    public static void UpdateUserEmail(UserModel user)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdate(u => u
                .SetProperty(u => u.Email, user.Email));
            db.SaveChanges();
        }
    }

    public static void DeleteUser(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
