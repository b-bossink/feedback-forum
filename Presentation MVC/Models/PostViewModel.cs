using System;
using System.Collections.Generic;

namespace Presentation_MVC.Models
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Upvotes { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        //public Dictionary<AttributeViewModel, string> ValuesByAttributes { get; set; }
        public List<PostAttributeViewModel> AttributesWithValue { get; set; }
    }
}
