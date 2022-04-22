using System.Security.Authentication;
using Logic.Containers;
using Logic.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Models.Users;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            ISession session = HttpContext.Session;
            string username = SessionExtensions.GetString(session, "Username");
            string password = SessionExtensions.GetString(session, "Password");
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());


            if (container.ValidateCredentials(username, password))
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

            if (container.ValidateCredentials(model.Username, model.Password))
            {
                SessionExtensions.SetString(session, "Username", model.Username);
                SessionExtensions.SetString(session, "Password", model.Password);

                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentialsMessage = "User not found. Please try again.";
            return View(model);
        }

        public IActionResult Register()
        {
            return RedirectToAction("Index");
        }

    }
}
