using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FeedbackForum.Classes
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

        public Post(string name, Category category, int id = -1)
        {
            ID = id;
            Name = name;
            CreationDate = DateTime.Now;
            Comments = new List<Comment>();
            Category = category;
            ValuesByAttributes = new Dictionary<Attribute, string>();
            foreach (Attribute attribute in category.Attributes)
            {
                ValuesByAttributes.Add(attribute, "");
            }
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
            if (ValuesByAttributes.ContainsKey(attribute))
            {
                ValuesByAttributes[attribute] = value;
            }
            else
            {
                MessageBox.Show("Couldn't find attribute with key " + "'" + attribute.Name + "'.");
            }
        }
    }
}
