using System;
using System.Collections.Generic;
using Presentation_MVC.Models.Users;

namespace Presentation_MVC.Models.Posting
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Upvotes { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public List<PostAttributeViewModel> AttributesWithValue { get; set; }
        public MemberViewModel Owner { get; set; }
    }
}
