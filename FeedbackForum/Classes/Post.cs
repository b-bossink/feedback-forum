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
        public Category Category { get; private set; }
        public Dictionary<string,string> Attributes { get; private set; }

        public Post(string name, Category category)
        {
            Name = name;
            CreationDate = DateTime.Now;
            Comments = new List<Comment>();
            Category = category;
            Attributes = new Dictionary<string, string>();
            foreach (string attributeKey in category.AttributeNames)
            {
                Attributes.Add(attributeKey, "");
            }
        }

        public void Add(Comment comment)
        {
            Comments.Add(comment);
        }

        public void SetAttributeValue(string attributeName, string value)
        {
            if (Attributes.ContainsKey(attributeName))
            {
                Attributes[attributeName] = value;
            }
            else
            {
                MessageBox.Show("Couldn't find attribute with key " + "'" + attributeName + "'.");
            }
        }
    }
}
