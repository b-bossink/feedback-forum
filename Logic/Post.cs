using Interfaces.Logic;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using Interfaces;
using Logic.Users;

namespace Logic
{
    public class Post : IEntity<PostDTO>
    {   
        public int ID { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Comment> Comments { get; private set; }
        public int Upvotes { get; private set; }
        public Category Category { get; private set; }
        public Dictionary<Attribute,string> ValuesByAttributes { get; private set; }
        public Member Owner { get; private set; }

        private readonly IPostDAL _DAL;

        public Post(IPostDAL dal, string name, DateTime creationDate, List<Comment> comments, int upvotes,
            Category category, Dictionary<Attribute,string> valuesByAttribute, Member owner, int id = -1)
        {
            _DAL = dal;
            ID = id;
            Name = name;
            Upvotes = upvotes;
            CreationDate = creationDate;
            Comments = comments;
            Category = category;
            ValuesByAttributes = valuesByAttribute;
            Owner = owner;
        }

        public Post(PostDTO dto)
        {
            ID = dto.ID;
            Name = dto.Name;
            Upvotes = dto.Upvotes;
            CreationDate = dto.CreationDate;
            Comments = new List<Comment>();
            foreach(CommentDTO commentDTO in dto.Comments)
            {
                Comments.Add(new Comment(commentDTO));
            }
            Category = new Category(dto.Category);
            ValuesByAttributes = new Dictionary<Attribute, string>();
            foreach (KeyValuePair<AttributeDTO,string> kvp in dto.ValuesByAttributes)
            {
                ValuesByAttributes.Add(new Attribute(kvp.Key), kvp.Value);
            }
            Owner = new Member(dto.Owner);
        }

        public CommunicationResult Create()
        {
            try
            {
                int rowsSaved = _DAL.Upload(ToDTO());
                if (rowsSaved == 1)
                {
                    return CommunicationResult.Succes;
                }
                else
                {
                    return CommunicationResult.UnexpectedError;
                }
            } catch (System.Data.SqlClient.SqlException)
            {
                return CommunicationResult.UnexpectedError;
            }
        }

        public CommunicationResult Update()
        {
            try {
                if (!_DAL.Exists(ID))
                {
                    return CommunicationResult.PostNotFoundError;
                }

                int rowsSaved = _DAL.Update(ToDTO());
                if (rowsSaved == 1)
                {
                    return CommunicationResult.Succes;
                }
                else
                {
                    return CommunicationResult.UnexpectedError;
                }
            } catch (System.Data.SqlClient.SqlException)
            {
                return CommunicationResult.UnexpectedError;
            }
        }
        
        public PostDTO ToDTO()
        {
            List<CommentDTO> comments = new List<CommentDTO>();
            foreach(Comment comment in Comments)
            {
                comments.Add(comment.ToDTO());
            }

            Dictionary<AttributeDTO, string> valueByAttribute = new Dictionary<AttributeDTO, string>();
            foreach(KeyValuePair<Attribute, string> pair in ValuesByAttributes)
            {
                valueByAttribute.Add(pair.Key.ToDTO(), pair.Value);
            }

            return new PostDTO
            {
                ID = this.ID,
                Name = this.Name,
                CreationDate = this.CreationDate,
                Comments = comments,
                Upvotes = this.Upvotes,
                Category = this.Category.ToDTO(),
                ValuesByAttributes = valueByAttribute,
                Owner = this.Owner.ToDTO()
            };  
        }
    }
}
