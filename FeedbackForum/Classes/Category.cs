using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class Category
    {
        public string Name { get; private set; }
        public Dictionary<string,string> Attributes { get; private set; }

        public Category(string Name, Dictionary<string,string> Attributes)
        {
            Name = "Test Categorie";
            Attributes = new Dictionary<string, string>
            {
                {
                    "Description", ""
                },
                {
                    "More Text", ""
                }
            };
        }

        public static List<Category> GetAll()
        {
            return new List<Category>()
            {
                new Category("", new Dictionary<string, string>()),
            };
        }

    }
}
