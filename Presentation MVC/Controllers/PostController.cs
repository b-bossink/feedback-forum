using Logic;
using Logic.Containers;
using Logic.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation_MVC.Converters;
using Presentation_MVC.Models.Posting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
            PostContainer container = new PostContainer(new DALFactory().GetPostDAL());
            PostViewModel postModel;
            try
            {
                postModel = ModelConverter.ToViewModel(container.Get(postId));
                return View(postModel);
            }
            catch
            {
                ViewBag.ErrorMessage = "ERROR: Post not found.";
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Create(int categoryId)
        {
            CategoryContainer container = new CategoryContainer(new DALFactory().GetCategoryDAL());
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
            CategoryContainer container = new CategoryContainer(new DALFactory().GetCategoryDAL());
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

        [HttpPost]
        public IActionResult Comment(int postId, string text)
        {
            Comment comment = new Comment(
                new DALFactory().GetCommentDAL(),
                text,
                DateTime.Now,
                0,
                new List<Comment>()
                );

            comment.Upload(postId);
            return RedirectToAction("ViewPost", new { postId = postId });
        }
    }
}
