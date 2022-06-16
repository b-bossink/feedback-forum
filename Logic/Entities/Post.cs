using Interfaces.DTOs;
using Interfaces;
using Data_Access;
using System;
using System.Collections.Generic;

namespace Logic.Entities
{
    public class Post : PostFactory
    {
        public Post(string name, DateTime creationDate, List<CommentFactory> comments, int upvotes, Category category, Dictionary<Attribute, string> valuesByAttribute, MemberFactory owner, int id = -1)
            : base(name, creationDate, comments, upvotes, category, valuesByAttribute, owner, id) { }

        public Post(PostDTO dto) : base(dto) {
        }

        protected override IPostDAL GetDAL()
        {
            return new PostDAL();
        }

        protected override CategoryFactory CreateCategory(CategoryDTO dto)
        {
            return new Category(dto);
        }

        protected override MemberFactory CreateMember(MemberDTO dto)
        {
            return new Member(dto);
        }

        protected override List<CommentFactory> CreateComments(List<CommentDTO> dtos)
        {
            List<CommentFactory> result = new List<CommentFactory>();

            foreach (CommentDTO commentDTO in dtos)
            {
                Comments.Add(new Comment(commentDTO));
            }
            return result;
        }
    }
}
