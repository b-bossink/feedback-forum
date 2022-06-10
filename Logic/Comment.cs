
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
        private readonly ICommentDAL _DAL;

        public Comment(ICommentDAL dal, string text, DateTime creationDate, int upvotes, List<Comment> replies, Member owner, int id = -1)
        {
            _DAL = dal;
            ID = id;
            Text = text;
            CreationDate = creationDate;
            Upvotes = upvotes;
            Replies = replies;
            Owner = owner;
        }

        public Comment(CommentDTO dto)
        {
            ID = dto.ID;
            Text = dto.Text;
            CreationDate = dto.CreationDate;
            Upvotes = dto.Upvotes;
            Replies = new List<Comment>();
            foreach (CommentDTO replyDTO in dto.Replies)
            {
                Replies.Add(new Comment(replyDTO));
            }
            Owner = new Member(dto.Owner);
        }

        public CommunicationResult Upload(int postID)
        {

            int savedRows = _DAL.Upload(ToDTO(), postID);

            if (savedRows == 1)
            { 
                return CommunicationResult.Succes;
            }

            if (savedRows == 404)
            {
                return CommunicationResult.PostNotFoundError;
            }

            return CommunicationResult.UnexpectedError;
        }

        public CommunicationResult Upload(int postID, int parentCommentID)
        {
            int savedRows = _DAL.Upload(ToDTO(), postID, parentCommentID);
            if (savedRows == 1)
            {
                return CommunicationResult.Succes;
            }

            return CommunicationResult.UnexpectedError;
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
                Replies = replies,
                Owner = this.Owner.ToDTO()
            };
        }
    }
}
