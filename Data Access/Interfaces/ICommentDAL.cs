using Data_Access;
using Data_Access.DTOs;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICommentDAL
    {
        public int Upload(CommentDTO comment, int postID);
        public int Upload(CommentDTO comment, int postID, int parentCommentID);
        public int Delete(int id);
        public List<CommentDTO> GetFromPost(int postID);
        public List<CommentDTO> GetFromComment(int parentCommentID);
        public int Update(CommentDTO comment);
    }
}
