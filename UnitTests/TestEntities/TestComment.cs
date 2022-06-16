using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;
using UnitTest.STUBs;

namespace UnitTest.TestEntities
{
	public class TestComment : CommentFactory
	{
        public TestComment(CommentDTO dto) : base(dto) { }
        

        public TestComment(string text, DateTime creationDate, int upvotes, List<CommentFactory> replies, Member owner, int id = -1)
            : base(text, creationDate, upvotes, replies, owner, id) { }

        protected override List<CommentFactory> CreateComments(List<CommentDTO> dtos)
        {
            List<CommentFactory> result = new List<CommentFactory>();
            foreach (CommentDTO dto in dtos)
            {
                result.Add(new TestComment(dto));
            }
            return result;
        }

        protected override ICommentDAL GetDAL()
        {
            return new CommentSTUB();
        }
    }
}

