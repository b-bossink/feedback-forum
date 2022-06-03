using Logic;
using Logic.Containers;
using Logic.Factories;
using Logic.Users;
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

        /// <summary>
        /// Loads the error view based on a CommunicationResult.
        /// </summary>
        /// <param name="result">A CommunicationResult</param>
        /// <exception cref="ArgumentException">Thrown when passed in enum is not of any "CommunicationResult" type.</exception>
        /// <example>Passing in <see cref="Post.CommunicationResult.PostNotFoundError"/> will show a view with an error message saying the post has not been found.</example>
        public static ErrorViewModel GenerateError(Enum result)
        {
            string error = "Something unexpected happened.";
            int code = -1;
            if (result is Post.CommunicationResult)
            {
                Post.CommunicationResult r = (Post.CommunicationResult)result;
                if (r == Post.CommunicationResult.PostNotFoundError)
                {
                    error = "The post you are trying to access doesn't exist.";
                }
                code = (int)r;
            } else
            if (result is Comment.CommunicationResult)
            {
                Comment.CommunicationResult r = (Comment.CommunicationResult)result;
                if (r == Comment.CommunicationResult.PostNotFoundError)
                {
                    error = "The post you are trying to access doesn't exist.";
                }
                code = (int)r;
            } else
            if (result is Member.CommunicationResult)
            {
                Member.CommunicationResult r = (Member.CommunicationResult)result;
                if (r == Member.CommunicationResult.DuplicateEmailError)
                {
                    error = "The emailadress already exists.";
                } else if (r == Member.CommunicationResult.DuplicateUsernameError)
                {
                    error = "The username already exists.";
                }
                code = (int)r;
            } else
            {
                throw new ArgumentException("Passed in enum must be of type CommunicationResult");
            }


            return new ErrorViewModel{ ErrorMessage = error, ErrorCode = code };
        }
    }
}
