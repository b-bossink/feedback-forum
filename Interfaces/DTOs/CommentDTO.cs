using System;
using System.Collections.Generic;

namespace Interfaces.DTOs
{
    public struct CommentDTO
    {
        public int ID;
        public string Text;
        public DateTime CreationDate;
        public int Upvotes;
        public List<CommentDTO> Replies;
        public MemberDTO Owner;
    }
}
