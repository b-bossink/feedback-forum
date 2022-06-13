using Interfaces;
using Logic;
using Logic.Containers;
using Logic.Factories;
using Logic.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Models.Users;

namespace Presentation_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemberDAL _memberDAL;
        public AccountController()
        {
            _memberDAL = (IMemberDAL)new MemberDALCreator().GetDAL();
        }

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
            MemberContainer container = new MemberContainer(_memberDAL);

            if (ModelState.IsValid) {
                Member member = container.Get(model.Username, model.Password);
                if (member != null)
                {
                    HttpContext.Session.SetInt32("ID", member.ID);
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("Password", model.Password);
                    return RedirectToAction("Index");
                }
                ViewBag.InvalidCredentialsMessage = "Invalid username and password combination. Please try again.";
                return View(model);
            }
            return RedirectToAction("Login");

        }

        public IActionResult Register()
        {
            if (ValidateCurrentSession(HttpContext))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.PasswordConfirmation)
                {
                    Member newUser = new Member(_memberDAL, model.Username, model.Emailaddress, model.Password);
                    CommunicationResult result = newUser.Register();

                    if (result == CommunicationResult.DuplicateUsernameError)
                    {
                        ViewBag.InvalidCredentialsMessage = "Username already taken. Please try another name.";
                        return View(model);
                    }

                    if (result == CommunicationResult.DuplicateEmailError)
                    {
                        ViewBag.InvalidCredentialsMessage = "Email address already taken. Please try another one.";
                        return View(model);
                    }

                    if (result == CommunicationResult.UnexpectedError)
                    {
                        ViewBag.InvalidCredentialsMessage = "An unexpected error occurred. Please try again.";
                        return View(model);
                    }

                    return RedirectToAction("Login");
                }
                ViewBag.InvalidCredentialsMessage = "Passwords did not match. Please try again.";
                return View(model);
            }
            return View(model);
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
