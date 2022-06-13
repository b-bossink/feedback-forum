using System;
using System.Collections.Generic;

namespace Interfaces.DTOs
{
    public class CommentDTO : DTO
    {
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Upvotes { get; set; }
        public List<CommentDTO> Replies { get; set; }
        public MemberDTO Owner { get; set; }
    }
}
