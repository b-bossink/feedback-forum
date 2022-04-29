using System.Diagnostics;
using System.Security.Authentication;
using Logic.Containers;
using Logic.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            ISession session = HttpContext.Session;
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());

            if (ModelState.IsValid) {
                if (container.ValidateCredentials(model.Username, model.Password))
                {
                    SessionExtensions.SetString(session, "Username", model.Username);
                    SessionExtensions.SetString(session, "Password", model.Password);

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

        public static bool ValidateCurrentSession(HttpContext context)
        {
            ISession session = context.Session;
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());
            return container.ValidateCredentials(
                SessionExtensions.GetString(session, "Username"),
                SessionExtensions.GetString(session, "Password"));
        }

    }
}
