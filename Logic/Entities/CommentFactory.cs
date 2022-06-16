using Interfaces.Logic;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using Interfaces;

namespace Logic.Entities
{
    public abstract class CommentFactory : IEntity<CommentDTO>
    {
        public int ID { get; private set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int Upvotes { get; private set; }
        public List<CommentFactory> Replies { get; private set; }
        public Member Owner { get; private set; }

        public CommentFactory(string text, DateTime creationDate, int upvotes, List<CommentFactory> replies, Member owner, int id = -1)
        {
            ID = id;
            Text = text;
            CreationDate = creationDate;
            Upvotes = upvotes;
            Replies = replies;
            Owner = owner;
        }

        public CommentFactory(CommentDTO dto)
        {
            ID = dto.ID;
            Text = dto.Text;
            CreationDate = dto.CreationDate;
            Upvotes = dto.Upvotes;
            Replies = CreateComments(dto.Replies);
            Owner = new Member(dto.Owner);
        }

        public CommunicationResult Create(int postID)
        {

            int savedRows = GetDAL().Upload(ToDTO(), postID);

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

        public CommunicationResult Create(int postID, int parentCommentID)
        {
            int savedRows = GetDAL().Upload(ToDTO(), postID, parentCommentID);
            if (savedRows == 1)
            {
                return CommunicationResult.Succes;
            }

            return CommunicationResult.UnexpectedError;
        }

        public CommentDTO ToDTO()
        {
            List<CommentDTO> replies = new List<CommentDTO>();
            foreach (CommentFactory comment in Replies)
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

        public CommunicationResult Create()
        {
            throw new NotImplementedException();
        }

        public CommunicationResult Update()
        {
            throw new NotImplementedException();
        }

        protected abstract ICommentDAL GetDAL();
        protected abstract List<CommentFactory> CreateComments(List<CommentDTO> dtos);
    }
}
