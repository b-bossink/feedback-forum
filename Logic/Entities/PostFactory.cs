using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;
using Interfaces.Logic;

namespace Logic.Entities
{
    public abstract class PostFactory : IEntity<PostDTO>
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<CommentFactory> Comments { get; private set; }
        public int Upvotes { get; private set; }
        public CategoryFactory Category { get; private set; }
        public Dictionary<Attribute, string> ValuesByAttributes { get; private set; }
        public MemberFactory Owner { get; private set; }

        protected PostFactory(string name, DateTime creationDate, List<CommentFactory> comments, int upvotes,
            CategoryFactory category, Dictionary<Attribute, string> valuesByAttribute, MemberFactory owner, int id = -1)
        {
            ID = id;
            Name = name;
            Upvotes = upvotes;
            CreationDate = creationDate;
            Comments = comments;
            Category = category;
            ValuesByAttributes = valuesByAttribute;
            Owner = owner;
        }

        protected PostFactory(PostDTO dto)
        {
            ID = dto.ID;
            Name = dto.Name;
            Upvotes = dto.Upvotes;
            CreationDate = dto.CreationDate;
            ValuesByAttributes = new Dictionary<Attribute, string>();
            foreach (KeyValuePair<AttributeDTO, string> kvp in dto.ValuesByAttributes)
            {
                ValuesByAttributes.Add(new Attribute(kvp.Key), kvp.Value);
            }
            Comments = CreateComments(dto.Comments);
            Category = CreateCategory(dto.Category);
            Owner = CreateMember(dto.Owner);
        }

        public CommunicationResult Create()
        {
            try
            {
                int rowsSaved = GetDAL().Upload(ToDTO());
                if (rowsSaved == 1)
                {
                    return CommunicationResult.Succes;
                }
                else
                {
                    return CommunicationResult.UnexpectedError;
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return CommunicationResult.UnexpectedError;
            }
        }

        public CommunicationResult Update()
        {
            try
            {
                if (!GetDAL().Exists(ID))
                {
                    return CommunicationResult.PostNotFoundError;
                }

                int rowsSaved = GetDAL().Update(ToDTO());
                if (rowsSaved == 1)
                {
                    return CommunicationResult.Succes;
                }
                else
                {
                    return CommunicationResult.UnexpectedError;
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return CommunicationResult.UnexpectedError;
            }
        }

        public PostDTO ToDTO()
        {
            List<CommentDTO> comments = new List<CommentDTO>();
            foreach (CommentFactory comment in Comments)
            {
                comments.Add(comment.ToDTO());
            }

            Dictionary<AttributeDTO, string> valueByAttribute = new Dictionary<AttributeDTO, string>();
            foreach (KeyValuePair<Attribute, string> pair in ValuesByAttributes)
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

        protected abstract IPostDAL GetDAL();
        protected abstract CategoryFactory CreateCategory(CategoryDTO dto);
        protected abstract MemberFactory CreateMember(MemberDTO dto);
        protected abstract List<CommentFactory> CreateComments(List<CommentDTO> dtos);
    }
}
