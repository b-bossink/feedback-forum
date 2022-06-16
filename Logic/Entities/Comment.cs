﻿using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Entities
{
    public class Comment : CommentFactory
    {
        public Comment(string text, DateTime creationDate, int upvotes, List<CommentFactory> replies, Member owner, int id = -1)
            : base(text, creationDate, upvotes, replies, owner, id) { }

        public Comment(CommentDTO dto) : base(dto) { }

        protected override ICommentDAL GetDAL()
        {
            throw new NotImplementedException();
        }

        protected override List<CommentFactory> CreateComments(List<CommentDTO> dtos)
        {
            List<CommentFactory> result = new List<CommentFactory>();

            foreach (CommentDTO commentDTO in dtos)
            {
                result.Add(new Comment(commentDTO));
            }
            return result;
        }
    }
}

