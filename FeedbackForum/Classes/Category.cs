using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class Category
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string,string> Attributes { get; private set; }

        public Category(string name, Dictionary<string,string> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

    }
}
