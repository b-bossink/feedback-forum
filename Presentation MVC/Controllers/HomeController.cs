using Logic;
using Logic.Containers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation_MVC.Models;
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
            PostContainer container = new PostContainer();
            List<PostViewModel> postModels = new List<PostViewModel>();
            foreach (Post post in container.GetAll())
            {
                postModels.Add(ToPostModel(post));
            }
            return View(postModels);
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

        private PostViewModel ToPostModel(Post post)
        {
            List<AttributeViewModel> categoryAttributes = new List<AttributeViewModel>();
            List<CommentViewModel> comments = new List<CommentViewModel>();
            Dictionary<AttributeViewModel, string> keyValuePairs = new Dictionary<AttributeViewModel, string>();

            foreach (Logic.Attribute attribute in post.Category.Attributes)
            {
                categoryAttributes.Add(ToAttrubiteModel(attribute));
            }

            foreach (Comment comment in post.Comments)
            {
                comments.Add(ToCommentModel(comment));
            }


            return new PostViewModel
            {
                ID = post.ID,
                Name = post.Name,
                CreationDate = post.CreationDate,
                Upvotes = post.Upvotes,
                Category = ToCategoryModel(post.Category),
                Comments = new List<CommentViewModel>(),
                ValuesByAttributes = new Dictionary<AttributeViewModel, string>()
            };
        }

        private CategoryViewModel ToCategoryModel(Category category)
        {
            return new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name,
                Attributes = new List<AttributeViewModel>()
            };
        }

        private AttributeViewModel ToAttrubiteModel(Logic.Attribute attribute)
        {
            return new AttributeViewModel()
            {
                ID = attribute.ID,
                Name = attribute.Name
            };
        }

        private CommentViewModel ToCommentModel(Comment comment)
        {
            List<CommentViewModel> replies = new List<CommentViewModel>();
            foreach (Comment reply in comment.Replies)
            {
                replies.Add(ToCommentModel(reply));
            }
            return new CommentViewModel()
            {
                ID = comment.ID,
                Text = comment.Text,
                CreationDate = comment.CreationDate,
                Upvotes = comment.Upvotes,
                Replies = replies
            };
        }
    }
}
