using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLearningCenter.WebSite.Models
{
    public class UserViewModel
    {
        public UserModel User { get; set; }
        public ClassModel[] Classes { get; set; }
        public UserClasses[] UserClasses { get; set; }
    }
}