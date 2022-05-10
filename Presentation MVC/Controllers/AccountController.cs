using System.Diagnostics;
using System.Security.Authentication;
using Logic.Containers;
using Logic.Factories;
using Logic.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Converters;
using Presentation_MVC.Models.Users;

namespace Presentation_MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            if (ValidateCurrentSession(HttpContext))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(MemberViewModel model)
        {
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());

            if (ModelState.IsValid) {
                if (container.ValidateCredentials(model.Username, model.Password))
                {
                    Member member = container.Get(model.Username, model.Password);
                    HttpContext.Session.SetInt32("ID", member.ID);
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("Password", model.Password);

                    return RedirectToAction("Index");
                }
                ViewBag.InvalidCredentialsMessage = "User not found. Please try again.";
                return View(model);
            }
            return RedirectToAction("Login");

        }

        public IActionResult Register()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Logout() {
            HttpContext.Session.Remove("ID");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Password");
            return RedirectToAction("Index", "Home");
        }

        public static bool ValidateCurrentSession(HttpContext context) {
            
            if (context.Session.GetInt32("ID") == null) { 
                return false;
            }

            return true;
        }

    }
}
