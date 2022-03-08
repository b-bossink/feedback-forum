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
                    new List<string>
                        {
                            {
                                "Description"
                            },
                            {
                                "More Text"
                            }
                        }
                    ),

                new Category(
                    "Muziek",
                    new List<string>
                        {
                            {
                                "Omschrijving"
                            },
                            {
                                "URL naar muziek"
                            },
                            {
                                "Mijn bijdrage"
                            },
                            {
                                "Lyrics"
                            },
                            {
                                "Genre"
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
