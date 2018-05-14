using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLearningCenter.WebSite
{
    public interface IClassRepository
    {
        ClassModel[] GetAll();
        ClassModel[] GetClass(int classId);
    }

    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }

    public class ClassRepository:IClassRepository
    {
        public ClassModel[] GetAll()
        {
            var classes = DatabaseAccessor.Instance.Classes.Select(t => new ClassModel { ClassId = t.ClassId, ClassName = t.ClassName, ClassPrice = t.ClassPrice, ClassDescription = t.ClassDescription }).ToArray();
            return classes;
        }

        public ClassModel[] GetClass(int classId)
        {
            var classes = DatabaseAccessor.Instance.Classes.Where(t => t.ClassId == classId).Select(t => new ClassModel { ClassId = t.ClassId, ClassName = t.ClassName, ClassPrice = t.ClassPrice, ClassDescription = t.ClassDescription }).ToArray();
            return classes;
        }

        
    }
}