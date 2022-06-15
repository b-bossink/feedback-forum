using System.Collections.Generic;
using Interfaces;
using Interfaces.Logic;
using Logic;
using Logic.Containers;
using Logic.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation_MVC.Converters;
using Presentation_MVC.Models.Posting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryDAL _categoryDAL;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoryDAL = (ICategoryDAL)new CategoryDALCreator().GetDAL();
        }

        public IActionResult Select() {
            if (!AccountController.ValidateCurrentSession(HttpContext))
                return RedirectToAction("Login", "Account");

            CategoryContainer container = new CategoryContainer(_categoryDAL);
            List<Category> categories = container.GetAll();

            if (categories != null)
            {
                List<CategoryViewModel> categoryModels = new List<CategoryViewModel>();
                foreach (Category category in categories)
                {
                    categoryModels.Add(ModelConverter.ToViewModel(category));
                }
                return View(categoryModels);
            }
            return View(new List<CategoryViewModel>());
        }
    }
}

