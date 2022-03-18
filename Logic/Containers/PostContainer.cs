using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class PostContainer
    {
        public List<Post> Posts { get; private set; }
        private PostDAL DAL = new PostDAL();

        public PostContainer()
        {
            Posts = new List<Post>();
            Refresh();
        }

        private Post ToPost(PostDTO dto)
        {
            List<Comment> comments = new List<Comment>();
            foreach (CommentDTO commentDTO in dto.Comments)
            {
                comments.Add(ToComment(commentDTO));
            }

            Dictionary<Attribute, string> valuesByAttribute = new Dictionary<Attribute, string>();
            foreach (KeyValuePair<AttributeDTO, string> dtoAttributes in dto.ValuesByAttributes)
            {
                valuesByAttribute.Add(ToAttribute(dtoAttributes.Key), dtoAttributes.Value);
            }

            return new Post(
                dto.Name,
                dto.CreationDate,
                comments,
                dto.Upvotes,
                ToCategory(dto.Category),
                valuesByAttribute,
                dto.ID
                );
        }

        private Category ToCategory(CategoryDTO dto)
        {
            List<Attribute> attributes = new List<Attribute>();
            foreach (AttributeDTO attributeDTO in dto.Attributes)
            {
                attributes.Add(ToAttribute(attributeDTO));
            }
            return new Category(
                dto.Name,
                attributes
                );
        }

        private Comment ToComment(CommentDTO dto)
        {
            List<Comment> replies = new List<Comment>();
            foreach (CommentDTO reply in dto.Replies)
            {
                replies.Add(ToComment(reply));
            }
            return new Comment(
                dto.Text,
                dto.CreationDate,
                dto.Upvotes,
                replies,
                dto.ID);
        }

        private Attribute ToAttribute(AttributeDTO dto)
        {
            return new Attribute(
                dto.Name,
                dto.ID
                );
        }

        public void Refresh()
        {
            Posts.Clear();
            foreach (PostDTO dto in DAL.LoadAll())
            {
                Posts.Add(ToPost(dto));
            }
        }
    }
}
