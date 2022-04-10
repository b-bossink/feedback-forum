using Data_Access;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Post
    {   
        public int ID { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Comment> Comments { get; private set; }
        public int Upvotes { get; private set; }
        public Category Category { get; private set; }
        public Dictionary<Attribute,string> ValuesByAttributes { get; private set; }
        private IPostDAL DAL;

        public Post(IPostDAL dal, string name, DateTime creationDate, List<Comment> comments, int upvotes,
            Category category, Dictionary<Attribute,string> valuesByAttribute, int id = -1)
        {
            DAL = dal;
            ID = id;
            Name = name;
            Upvotes = upvotes;
            CreationDate = creationDate;
            Comments = comments;
            Category = category;
            ValuesByAttributes = valuesByAttribute;
        }

        public Post(IPostDAL dal, ICategoryDAL categoryDAL, PostDTO dto)
        {
            DAL = dal;
            ID = dto.ID;
            Name = dto.Name;
            Upvotes = dto.Upvotes;
            CreationDate = dto.CreationDate;
            Comments = new List<Comment>();
            foreach(CommentDTO commentDTO in dto.Comments)
            {
                Comments.Add(new Comment(commentDTO));
            }
            Category = new Category(categoryDAL, dto.Category);
            ValuesByAttributes = new Dictionary<Attribute, string>();
            foreach (KeyValuePair<AttributeDTO,string> kvp in dto.ValuesByAttributes)
            {
                ValuesByAttributes.Add(new Attribute(kvp.Key), kvp.Value);
            }
        }

        public int Upload()
        {
            return DAL.Upload(ToDTO());
        }

        private PostDTO ToDTO()
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
                ValuesByAttributes = valueByAttribute
            };  
        }
    }
}
