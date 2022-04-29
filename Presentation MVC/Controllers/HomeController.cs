﻿using Logic;
using Logic.Containers;
using Logic.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation_MVC.Converters;
using Presentation_MVC.Models;
using Presentation_MVC.Models.Posting;
using Presentation_MVC.Models.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PostContainer container = new PostContainer(new DALFactory().GetPostDAL());
            List<PostViewModel> postModels = new List<PostViewModel>();
            List<Post> posts = container.GetAll();
            if (posts != null)
            {
                foreach (Post post in posts)
                {
                    postModels.Add(ModelConverter.ToViewModel(post));
                }
                return View(postModels);
            }
            return View(new List<PostViewModel>());

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
