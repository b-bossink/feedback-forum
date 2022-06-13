using System;
using System.Collections.Generic;

namespace Interfaces.DTOs
{
    public class PostDTO : DTO
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Upvotes { get; set; }
        public CategoryDTO Category { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public Dictionary<AttributeDTO, string> ValuesByAttributes { get; set; }
        public MemberDTO Owner { get; set; }
    }
}
