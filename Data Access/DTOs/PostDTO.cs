using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access
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
    }
}
