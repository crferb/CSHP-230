using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheLearningCenter.Models;

namespace TheLearningCenter
{
    public interface IUserRepository
    {
        User LogIn(string email, string password);
    }

    //public class UserRepository : IUserRepository
    //{
        //public IEnumerable<User> Users
        //{
            
        //}

        //public User LogIn(string email, string password)
        //{
        //    return Users.SingleOrDefault(t => t.UserEmail.ToLower() == email.ToLower() && t.UserPassword == password);
        //}
    //}
}