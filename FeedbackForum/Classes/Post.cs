using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FeedbackForum.Classes
{
    class Post
    {   public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Comment> Comments { get; private set; }
        public Category Category { get; private set; }

        public Post(string name, DateTime creationDate, List<Comment> comments, Category category)
        {
            Name = name;
            CreationDate = creationDate;
            Comments = comments;
            Category = category;
        }

        public void SetAttributeValue(string attributeName, string value)
        {
            if (Category.Attributes.ContainsKey(attributeName))
            {
                Category.Attributes[attributeName] = value;
            }
            else
            {
                MessageBox.Show("Couldn't find attribute with key " + "'" + attributeName + "'.");
            }
        }
    }
}
