using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLearningCenter.WebSite.Models;

namespace TheLearningCenter.WebSite
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
        UserModel[] GetUser(int userId);
        UserModel AddClass(int userId, int classId);
        ClassModel[] GetUserClasses(int userId);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool UserIsAdmin { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Email = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                    .Add(new User { UserEmail = email, UserPassword = password });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.UserId, Email = user.UserEmail };
        }
        
        public UserModel[] GetUser(int userId)
        {
            var user = DatabaseAccessor.Instance.Users.Where(t => t.UserId == userId).Select(t => new UserModel { Id = t.UserId, Email = t.UserEmail }).ToArray();
            return user;
        }

        public ClassModel[] GetUserClasses(int userId)
        {
           userId = 1;
           var user = DatabaseAccessor.Instance.Users.FirstOrDefault(t => t.UserId == userId);
           var userClasses = user.Classes.Select(t => new ClassModel { ClassId = t.ClassId, ClassName = t.ClassName, ClassPrice = t.ClassPrice, ClassDescription = t.ClassDescription }).ToArray();
           return userClasses;

        }

        public UserModel AddClass(int userId, int classId)
        {
            var user = DatabaseAccessor.Instance.Users.FirstOrDefault(t => t.UserId == userId);
            var studentClass = DatabaseAccessor.Instance.Classes.FirstOrDefault(t => t.ClassId == classId);

            user.Classes.Add(studentClass);

            return new UserModel { Id = user.UserId, Email = user.UserEmail};
        }
    }
}