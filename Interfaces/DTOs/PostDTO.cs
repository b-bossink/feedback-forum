using System;
using System.Collections.Generic;

namespace Interfaces.DTOs
{
    public struct PostDTO
    {
        public int ID;
        public string Name;
        public DateTime CreationDate;
        public int Upvotes;
        public CategoryDTO Category;
        public List<CommentDTO> Comments;
        public Dictionary<AttributeDTO, string> ValuesByAttributes;
        public MemberDTO Owner;
    }
}
