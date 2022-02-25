using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class CategoryContainer
    {
        public List<Category> Categories { get; private set; }
        private Database database;

        public CategoryContainer()
        {
            Categories = new List<Category>()
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

        public void Add(Category category)
        {

        }

        public void Edit(Category category)
        {

        }

        public void Delete(Category category)
        {

        }
    }
}
