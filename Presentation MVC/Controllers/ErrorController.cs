using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Models;

namespace Presentation_MVC.ErrorHandling
{
	public class ErrorController : Controller
	{
		public IActionResult Index(ErrorViewModel model)
        {
			return View(model);
        }
	}
}

