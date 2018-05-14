using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLearningCenter
{
    public interface IClassRepository
    {
        IEnumerable<Class> Classes { get; }
    }

    public class ClassRepository : IClassRepository
    {
        public IEnumerable<Class> Classes
        {
            get { }
        }
    }

    public void AddClassToUser(int userId, int classId)
    {
        var user = UserRepository.GetUser(userId);
        var classToAdd = ClassRepository.GetClass(classId);
        user.Classes.Add(classToAdd);
        DatabaseAccessor.SubmitChanges();
    }
}