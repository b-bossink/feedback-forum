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
            Post post = container.Get(postId);
            if (post == null)
            {
                ViewBag.ErrorMessage = "ERROR: Post not found.";
                return RedirectToAction("Error", "Home");
            }

            PostViewModel postModel;
            postModel = ModelConverter.ToViewModel(post);
            return View(postModel);
        }

        public IActionResult Create(int categoryId)
        {
            if (!AccountController.ValidateCurrentSession(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            CategoryContainer container = new CategoryContainer(_categoryDAL);
            Category category = container.Get(categoryId);
            List<PostAttributeViewModel> attributes = new List<PostAttributeViewModel>();
            foreach (Logic.Attribute attribute in category.Attributes)
            {
                attributes.Add(new PostAttributeViewModel()
                {
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

        [HttpPost]
        public IActionResult Create(PostViewModel post)
        {
            post.Comments = new List<CommentViewModel>();
            post.Upvotes = 0;
            post.CreationDate = DateTime.Now;
            MemberContainer memberContainer = new MemberContainer(_memberDAL);
            post.Owner = ModelConverter.ToViewModel(memberContainer.Get((int)SessionExtensions.GetInt32(HttpContext.Session, "ID")));
            Post postToUpload = ModelConverter.ToPost(post);
            if (postToUpload.Upload() != Post.CommunicationResult.Succes)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Home");
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

            Comment.CommunicationResult result = comment.Upload(postId);
            if (result == Logic.Comment.CommunicationResult.Succes)
            {
                return RedirectToAction("ViewPost", new { postId });
            }

            if (result == Logic.Comment.CommunicationResult.PostNotFoundError ||
                result == Logic.Comment.CommunicationResult.UnexpectedError)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        public IActionResult Delete(int postId)
        {
            if (!AccountController.ValidateCurrentSession(HttpContext))
            {
                return RedirectToAction("ViewPost", new { postId });
            }

            PostContainer container = new PostContainer(_postDAL);
            container.Delete(postId);
            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult Edit(int postId) {
            if (!AccountController.ValidateCurrentSession(HttpContext))
            { 
                return RedirectToAction("Login", "Account");
            }

            PostViewModel model = ModelConverter.ToViewModel(new PostContainer(_postDAL).Get(postId));
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PostViewModel newModel) {
            if (!AccountController.ValidateCurrentSession(HttpContext))
            { 
                return RedirectToAction("Login", "Account");
            }

            Post oldPost = new PostContainer(_postDAL).Get(newModel.ID);
            PostViewModel oldModel = ModelConverter.ToViewModel(oldPost);

            newModel.Owner = oldModel.Owner;
            newModel.Comments = oldModel.Comments;
            newModel.Upvotes = oldModel.Upvotes;
            newModel.CreationDate = oldModel.CreationDate;
            newModel.Category = oldModel.Category;

            Post newPost = ModelConverter.ToPost(newModel);
            Post.CommunicationResult result = newPost.Update();
            if (result == Post.CommunicationResult.Succes)
            {
                return RedirectToAction("ViewPost", new { postId = newModel.ID });
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
