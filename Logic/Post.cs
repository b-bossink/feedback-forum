using Data_Access;
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

        public Post(string name, DateTime creationDate, List<Comment> comments, int upvotes,
            Category category, Dictionary<Attribute,string> valuesByAttribute, int id = -1)
        {
            ID = id;
            Name = name;
            Upvotes = upvotes;
            CreationDate = creationDate;
            Comments = comments;
            Category = category;
            ValuesByAttributes = valuesByAttribute;
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
        }

        public void Upload()
        {
            new PostDAL().Upload(ToDTO());
        }

        public void Delete()
        {

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

        public void Add(Comment comment)
        {
            Comments.Add(comment);
        }

        public void Add(int upvote)
        {
            Upvotes += upvote;
        }

        public void Delete(Comment comment)
        {
            Comments.Remove(comment);
        }

        public void Delete(int upvote)
        {
            Upvotes -= upvote;
        }

        public void SetAttributeValue(Attribute attribute, string value)
        {
            Attribute attributeToEdit = null;

            foreach (KeyValuePair<Attribute,string> keyValuePair in ValuesByAttributes)
            {
                if (keyValuePair.Key.Name == attribute.Name)
                {
                    attributeToEdit = keyValuePair.Key;
                }
            }

            ValuesByAttributes[attributeToEdit] = value;
        }
    }
}
