using Logic;
using Logic.Containers;
using Logic.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation_MVC.Converters;
using Presentation_MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation_MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        public IActionResult ViewPost(int postId)
        {
            Debug.WriteLine($"Clicked on post with ID {postId}");
            PostContainer container = new PostContainer(new DALFactory().GetPostDAL());
            PostViewModel postModel;
            foreach (Post post in container.GetAll())
            {
                if (post.ID == postId)
                {
                    postModel = ModelConverter.ToViewModel(post);
                    return View(postModel);
                }
            }
            ViewBag.ErrorMessage = "ERROR: Post not found.";
            return RedirectToAction("Error", "Home");
        }

        public IActionResult Create(int categoryId)
        {
            CategoryContainer container = new CategoryContainer();
            foreach (Category category in container.GetAll())
            {
                if (category.ID == categoryId)
                {
                    List<PostAttributeViewModel> attributes = new List<PostAttributeViewModel>();
                    foreach (Logic.Attribute attribute in category.Attributes)
                    {
                        attributes.Add(new PostAttributeViewModel() { 
                            AttributeID = attribute.ID,
                            Value = "",
                            Name = attribute.Name
                        });
                    }
                    PostViewModel post = new PostViewModel()
                    {
                        Category = ModelConverter.ToViewModel(category),
                        AttributesWithValue = attributes
                    };
                    return View(post);
                }
            }
            ViewBag.Error = "Category couldn't be found.";
            return View("SelectCategory");

        }

        [HttpPost]
        public IActionResult Create(PostViewModel post)
        {
            post.Comments = new List<CommentViewModel>();
            post.Upvotes = 0;
            post.CreationDate = DateTime.Now;
            ModelConverter.ToPost(post).Upload();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SelectCategory()
        {
            CategoryContainer container = new CategoryContainer();
            List<CategoryViewModel> categoryModels = new List<CategoryViewModel>();
            foreach (Category category in container.GetAll())
            {
                categoryModels.Add(ModelConverter.ToViewModel(category));
            }
            return View(categoryModels);
        }

        
    }
}
