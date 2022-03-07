using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class Comment
    {
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int Upvotes { get; private set; }
        public List<Comment> Replies { get; private set; }

        public Comment(string text)
        {
            Text = text;
            CreationDate = DateTime.Now;
            Upvotes = 0;
            Replies = new List<Comment>();
        }

        public void AddReply(Comment comment)
        {
            Replies.Add(comment);
        }
    }
}
