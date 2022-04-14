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
            return View("Login");
        }

        public IActionResult Login(string nextView)
        {
            ISession session = HttpContext.Session;
            string username = SessionExtensions.GetString(session, "Username");
            string password = SessionExtensions.GetString(session, "Password");
            MemberContainer container = new MemberContainer(new DALFactory().GetMemberDAL());

            bool valid = container.Get(username, password) != null;

            if (!valid)
            {
                return View(nextView);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(MemberViewModel model)
        {
            ISession session = HttpContext.Session;
            SessionExtensions.SetString(session, "Username", model.Username);
            SessionExtensions.SetString(session, "Password", model.Password);
            
            return View();
        }

        public IActionResult ViewAfterValidating(string view, string controller)
        {
            return RedirectToAction(view, controller);
        }
    }
}
