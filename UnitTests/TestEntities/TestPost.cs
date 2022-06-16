using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;
using UnitTest.STUBs;

namespace UnitTest.TestEntities
{
	public class TestPost : PostFactory
	{
        public TestPost(string name, DateTime creationDate, List<CommentFactory> comments, int upvotes, CategoryFactory category, Dictionary<Logic.Entities.Attribute, string> valuesByAttribute, MemberFactory owner, int id = -1)
            : base(name, creationDate, comments, upvotes, category, valuesByAttribute, owner, id) { }

        public TestPost(PostDTO dto) : base(dto) { }

        protected override CategoryFactory CreateCategory(CategoryDTO dto)
        {
            return new TestCategory(dto);
        }

        protected override List<CommentFactory> CreateComments(List<CommentDTO> dtos)
        {
            List<CommentFactory> result = new List<CommentFactory>();
            foreach (CommentDTO dto in dtos)
            {
                result.Add(new TestComment(dto));
            }
            return result;
        }

        protected override MemberFactory CreateMember(MemberDTO dto)
        {
            return new Member(dto);
        }

        protected override IPostDAL GetDAL()
        {
            return new PostSTUB();
        }
    }
}

