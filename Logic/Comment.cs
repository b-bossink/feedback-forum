
using Interfaces;
using Interfaces.DTOs;
using Logic.Users;
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
        public Member Owner { get; private set; }
        private ICommentDAL DAL;

        public Comment(ICommentDAL dal, string text, DateTime creationDate, int upvotes, List<Comment> replies, int id = -1)
        {
            DAL = dal;
            ID = id;
            Text = text;
            CreationDate = creationDate;
            Upvotes = upvotes;
            Replies = replies;
        }

        public Comment(ICommentDAL dal, CommentDTO dto)
        {
            DAL = dal;
            ID = dto.ID;
            Text = dto.Text;
            CreationDate = dto.CreationDate;
            Upvotes = dto.Upvotes;
            Replies = new List<Comment>();
            foreach (CommentDTO replyDTO in dto.Replies)
            {
                Replies.Add(new Comment(dal, replyDTO));
            }
        }

        public int Upload(int postID)
        {
            return DAL.Upload(ToDTO(), postID);
        }

        public int Upload(int postID, int parentCommentID)
        {
            return DAL.Upload(ToDTO(), postID, parentCommentID);
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
