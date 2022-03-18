using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access
{
    public struct CommentDTO
    {
        public int ID;
        public string Text;
        public DateTime CreationDate;
        public int Upvotes;
        public List<CommentDTO> Replies;
    }
}
