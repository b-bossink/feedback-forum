using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Comment
    {
        public int ID { get; private set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int Upvotes { get; private set; }
        public List<Comment> Replies { get; private set; }

        public Comment(string text, DateTime creationDate, int upvotes, List<Comment> replies, int id = -1)
        {
            ID = id;
            Text = text;
            CreationDate = creationDate;
            Upvotes = upvotes;
            Replies = replies;
        }

        public void AddReply(Comment comment)
        {
            Replies.Add(comment);
        }

        public CommentDTO ToDTO()
        {
            List<CommentDTO> replies = new List<CommentDTO>();
            foreach (Comment comment in Replies)
            {
                replies.Add(comment.ToDTO());
            }

            return new CommentDTO
            {
                ID = this.ID,
                Text = this.Text,
                CreationDate = this.CreationDate,
                Upvotes = this.Upvotes,
                Replies = replies
            };
        }
    }
}
