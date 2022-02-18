using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class Category
    {
        public string Name { get; private set; }
        public Dictionary<string,string> Attributes { get; private set; }

        public Category(string name, Dictionary<string,string> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public static List<Category> GetAll()
        {
            return new List<Category>()
            {
                new Category(
                    "Test Categorie",
                    new Dictionary<string, string>
                        {
                            {
                                "Description", ""
                            },
                            {
                                "More Text", ""
                            }
                        }
                    ),

                new Category(
                    "Nog een test categorie!",
                    new Dictionary<string, string>
                        {
                            {
                                "Description", ""
                            },
                            {
                                "More Text", ""
                            }
                        }
                    )
            };
        }
    }
}
