using System;
using System.Collections.Generic;
using Presentation_MVC.Models.Users;

namespace Presentation_MVC.Models.Posting
{
    public class CommentViewModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Upvotes { get; set; }
        public List<CommentViewModel> Replies { get; set; }
        public MemberViewModel Owner { get; set; }
    }
}

