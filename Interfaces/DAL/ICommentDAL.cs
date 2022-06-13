using Interfaces.DTOs;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICommentDAL : IDAL<CommentDTO>
    {
        public int Upload(CommentDTO comment, int postID);
        public int Upload(CommentDTO comment, int postID, int parentCommentID);
        public List<CommentDTO> GetFromPost(int postID);
        public List<CommentDTO> GetFromComment(int parentCommentID);
    }
}
