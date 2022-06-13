using Interfaces;
using Logic;
using Logic.Factories;
using Logic.Users;
using Presentation_MVC.Models;
using Presentation_MVC.Models.Posting;
using Presentation_MVC.Models.Users;
using System.Collections.Generic;

namespace Presentation_MVC.Converters
{
    public static class ModelConverter
    {
        public static PostViewModel ToViewModel(Post post)
        {
            List<CommentViewModel> commentModels = new List<CommentViewModel>();
            List<PostAttributeViewModel> postAttributes = new List<PostAttributeViewModel>();

            foreach (KeyValuePair<Attribute, string> kvp in post.ValuesByAttributes)
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
        private static AttributeViewModel ToViewModel(Attribute attribute)
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
        public static ErrorViewModel ToViewModel(CommunicationResult result) {
            return new ErrorViewModel()
            {
                ErrorMessage = result.message,
                ErrorCode = result.code
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

            return new Post(
                (IPostDAL)new PostDALCreator().GetDAL(),
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

            return new Category(
                (ICategoryDAL)new CategoryDALCreator().GetDAL(),
                model.Name,
                attributes,
                model.ID);
        }
        private static Member ToMember(MemberViewModel model) {
            return new Member
            (
                (IMemberDAL)new MemberDALCreator().GetDAL(),
                model.Username,
                model.Emailaddress,
                model.Password,
                model.ID
            );
        }
        private static Comment ToComment(CommentViewModel model)
        {
            List<Comment> replies = new List<Comment>();
            foreach (CommentViewModel commentModel in model.Replies)
            {
                replies.Add(ToComment(commentModel));
            }
            return new Comment(
                (ICommentDAL)new CommentDALCreator().GetDAL(),
                model.Text,
                model.CreationDate,
                model.Upvotes,
                replies,
                ToMember(model.Owner),
                model.ID
                );
        }
        private static Attribute ToAttribute(AttributeViewModel model)
        {
            return new Logic.Attribute(model.Name, model.ID);
        }
        private static Attribute ToAttribute(PostAttributeViewModel model)
        {
            return new Logic.Attribute(model.Name, model.AttributeID);
        }


    }
}
