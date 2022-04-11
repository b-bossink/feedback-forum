using Data_Access;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICommentDAL
    {
        public int Upload(CommentDTO comment, int parentPostID, int? parentCommentID = null);
        public int Delete(int id);
        public int Update(CommentDTO comment);
    }
}
