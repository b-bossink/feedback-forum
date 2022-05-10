using Interfaces;
using Logic;
using Logic.Containers;
using Logic.Factories;
using Logic.Users;
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
        private readonly IPostDAL _postDAL;
        private readonly ICategoryDAL _categoryDAL;
        private readonly IMemberDAL _memberDAL;
        private readonly ICommentDAL _commentDAL;


        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;

            DALFactory factory = new DALFactory();
            _postDAL = factory.GetPostDAL();
            _categoryDAL = factory.GetCategoryDAL();
            _memberDAL = factory.GetMemberDAL();
            _commentDAL = factory.GetCommentDAL();
        }

        public IActionResult ViewPost(int postId)
        {
            PostContainer container = new PostContainer(_postDAL);
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
            if (!AccountController.ValidateCurrentSession(HttpContext))
                return RedirectToAction("Login", "Account");

            CategoryContainer container = new CategoryContainer(_categoryDAL);
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

            MemberContainer memberContainer = new MemberContainer(_memberDAL);
            post.Owner = ModelConverter.ToViewModel(memberContainer.Get((int)SessionExtensions.GetInt32(HttpContext.Session, "ID")));
            ModelConverter.ToPost(post).Upload();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SelectCategory()
        {
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

        [HttpPost]
        public IActionResult Comment(int postId, string text)
        {
            Member owner = new MemberContainer(_memberDAL).Get((int)HttpContext.Session.GetInt32("ID"));
            Comment comment = new Comment(
                _commentDAL,
                text,
                DateTime.Now,
                0,
                new List<Comment>(),
                owner
                );

            comment.Upload(postId);
            return RedirectToAction("ViewPost", new { postId = postId });
        }

        public IActionResult Delete(int postId) {
            if (AccountController.ValidateCurrentSession(HttpContext)) {
                PostContainer container = new PostContainer(_postDAL);
                container.Delete(postId);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ViewPost", new { postId = postId});
        }
    }
}
