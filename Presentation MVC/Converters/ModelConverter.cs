using Logic;
using Logic.Containers;
using Logic.Factories;
using Logic.Users;
using Presentation_MVC.Models;
using Presentation_MVC.Models.Posting;
using Presentation_MVC.Models.Users;
using System;
using System.Collections.Generic;
namespace Presentation_MVC.Converters
{
    public static class ModelConverter
    {
        public static PostViewModel ToViewModel(Post post)
        {
            List<CommentViewModel> commentModels = new List<CommentViewModel>();
            List<PostAttributeViewModel> postAttributes = new List<PostAttributeViewModel>();

            foreach (KeyValuePair<Logic.Attribute, string> kvp in post.ValuesByAttributes)
            {
                postAttributes.Add(new PostAttributeViewModel
                {
                    AttributeID = kvp.Key.ID,
                    Name = kvp.Key.Name,
                    Value = kvp.Value
                });
            }

            foreach (Comment comment in post.Comments)
            {
                commentModels.Add(ToViewModel(comment));
            }


            return new PostViewModel
            {
                ID = post.ID,
                Name = post.Name,
                CreationDate = post.CreationDate,
                Upvotes = post.Upvotes,
                Category = ToViewModel(post.Category),
                Comments = commentModels,
                AttributesWithValue = postAttributes,
                Owner = ToViewModel(post.Owner)
            };
        }
        private static CommentViewModel ToViewModel(Comment comment)
        {
            List<CommentViewModel> replies = new List<CommentViewModel>();
            foreach (Comment reply in comment.Replies)
            {
                replies.Add(ToViewModel(reply));
            }

            return new CommentViewModel()
            {
                ID = comment.ID,
                Text = comment.Text,
                CreationDate = comment.CreationDate,
                Upvotes = comment.Upvotes,
                Replies = replies,
                Owner = ToViewModel(comment.Owner)
            };
        }
        public static CategoryViewModel ToViewModel(Category category)
        {
            List<AttributeViewModel> attributeModels = new List<AttributeViewModel>();
            foreach (Logic.Attribute attribute in category.Attributes)
            {
                attributeModels.Add(ToViewModel(attribute));
            }
            return new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name,
                Attributes = attributeModels
            };
        }

        private static AttributeViewModel ToViewModel(Logic.Attribute attribute)
        {
            return new AttributeViewModel()
            {
                ID = attribute.ID,
                Name = attribute.Name
            };
        }

        public static MemberViewModel ToViewModel(Member member) {
            return new MemberViewModel
            {
                ID = member.ID,
                Username = member.Username,
                Emailaddress = member.Emailaddress,
                Password = member.Password
            };
        }

        public static Post ToPost(PostViewModel model)
        {
            Dictionary<Logic.Attribute, string> attributeWithValues = new Dictionary<Logic.Attribute, string>();

            if (model.AttributesWithValue != null)
            {
                foreach (PostAttributeViewModel postAttribute in model.AttributesWithValue)
                {
                    attributeWithValues.Add(
                        ToAttribute(postAttribute), postAttribute.Value);
                }
            } else
            {
                model.AttributesWithValue = new List<PostAttributeViewModel>();
            }

            List<Comment> comments = new List<Comment>();
            foreach (CommentViewModel commentModel in model.Comments)
            {
                comments.Add(ToComment(commentModel));
            }

            DALFactory factory = new DALFactory();
            return new Post(
                factory.GetPostDAL(),
                model.Name,
                model.CreationDate,
                comments,
                model.Upvotes,
                ToCategory(model.Category),
                attributeWithValues,
                ToMember(model.Owner),
                model.ID
            );
        }

        private static Category ToCategory(CategoryViewModel model)
        {
            List<Logic.Attribute> attributes = new List<Logic.Attribute>();
            if (model.Attributes != null)
            {
                foreach (AttributeViewModel attributeModel in model.Attributes)
                {
                    attributes.Add(ToAttribute(attributeModel));
                }
            } 
            else
            {
                model.Attributes = new List<AttributeViewModel>();
            }

            DALFactory factory = new DALFactory();
            return new Category(
                factory.GetCategoryDAL(),
                model.Name,
                attributes,
                model.ID);
        }

        private static Member ToMember(MemberViewModel model) {
            return new Member
            (
                model.ID,
                model.Username,
                model.Emailaddress,
                model.Password
            );
        }

        private static Comment ToComment(CommentViewModel model)
        {
            List<Comment> replies = new List<Comment>();
            foreach (CommentViewModel commentModel in model.Replies)
            {
                replies.Add(ToComment(commentModel));
            }
            DALFactory factory = new DALFactory();
            return new Comment(
                factory.GetCommentDAL(),
                model.Text,
                model.CreationDate,
                model.Upvotes,
                replies,
                ToMember(model.Owner),
                model.ID
                );
        }

        private static Logic.Attribute ToAttribute(AttributeViewModel model)
        {
            return new Logic.Attribute(model.Name, model.ID);
        }

        private static Logic.Attribute ToAttribute(PostAttributeViewModel model)
        {
            return new Logic.Attribute(model.Name, model.AttributeID);
        }


    }
}
