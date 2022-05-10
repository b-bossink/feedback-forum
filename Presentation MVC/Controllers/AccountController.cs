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
            ISession session = HttpContext.Session;
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());

            if (ModelState.IsValid) {
                if (container.ValidateCredentials(model.Username, model.Password))
                {
                    Member member = container.Get(model.Username, model.Password);
                    SessionExtensions.SetInt32(session, "ID", member.ID);
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

        /*
        public static MemberViewModel GetCurrentMember(HttpContext context)
        {
            ISession session = context.Session;
            string username = SessionExtensions.GetString(session, "Username");
            string password = SessionExtensions.GetString(session, "Password");
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());
            if(container.ValidateCredentials(username, password))
            {
                return ModelConverter.ToViewModel(container.Get(username, password));
            }
            return null;
        }
        */
        public static bool ValidateCurrentSession(HttpContext context) {
            if (SessionExtensions.GetInt32(context.Session, "ID") == null) { 
                return false;
            }

            return true;
        }

    }
}
