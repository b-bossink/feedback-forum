using System;
using System.Collections.Generic;

namespace Presentation_MVC.Models
{
    public class CommentViewModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Upvotes { get; set; }
        public int Post { get; set; }
        public List<CommentViewModel> Replies { get; set; }
    }
}

