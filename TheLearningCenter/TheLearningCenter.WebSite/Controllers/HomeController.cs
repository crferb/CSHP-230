using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheLearningCenter.WebSite.Models;

namespace TheLearningCenter.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IClassRepository classRepository;

        public HomeController(IUserRepository userRepository, IClassRepository classRepository)
        {
            this.userRepository = userRepository;
            this.classRepository = classRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ClassList()
        {
            var items = classRepository.GetAll().Select(t => new TheLearningCenter.WebSite.Models.ClassModel { Id = (int)t.ClassId, Name = t.ClassName, Price = t.ClassPrice, Description= t.ClassDescription }).ToArray();
            return View(items);
        }

        public ActionResult EnrollInClass(string returnUrl)
        {
            if (Session["User"] == null)
                return Redirect(returnUrl ?? "~/home/login");
            else
            {
                var items = classRepository.GetAll().Select(t => new TheLearningCenter.WebSite.Models.ClassModel { Id = (int)t.ClassId, Name = t.ClassName, Price = t.ClassPrice, Description = t.ClassDescription }).ToArray();
                return View(items);
            }
        }

        public ActionResult StudentClasses(string returnUrl, int userId = 0)
        {
            if (Session["User"] == null)
            {
                userId = 0;
                return Redirect(returnUrl ?? "~/home/login");
            }
            else
            {
                var user = (TheLearningCenter.WebSite.Models.UserModel)Session["User"];
                userId = user.Id;
                var studentClasses = userRepository.GetUserClasses(userId).Select(t => new TheLearningCenter.WebSite.Models.ClassModel { Id = (int)t.ClassId, Name = t.ClassName, Price = t.ClassPrice, Description = t.ClassDescription }).ToArray();
                return View(studentClasses);
            }
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new TheLearningCenter.WebSite.Models.UserModel { Id = user.Id, Email = user.Email };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        [HttpPost]
        public ActionResult Register(RegisterView registerView, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.Register(registerView.Email, registerView.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new TheLearningCenter.WebSite.Models.UserModel { Id = user.Id, Email = user.Email };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(registerView.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(registerView);
        }

    }
}